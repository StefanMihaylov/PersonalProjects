using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using ChatServer.Common.Models;
using ChatServer.Core.Interfaces;

namespace ChatServer.Core.Managers
{
    public class MessagesManager : IMessagesManager
    {
        private readonly IMessagesProvider m_MessagerProvider;
        private readonly ILogger m_Logger;
        private readonly ConcurrentDictionary<int, SortedDictionary<DateTime, Message>> m_MessagesByRoomId;

        public MessagesManager(IMessagesProvider messagesProvider, ILogger logger)
        {
            if (messagesProvider == null)
            {
                throw new ArgumentNullException("messagesProvider", "MessagesProvider is null");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger", "Logger is null");
            }

            m_MessagerProvider = messagesProvider;
            m_Logger = logger;
            m_MessagesByRoomId = new ConcurrentDictionary<int, SortedDictionary<DateTime, Message>>();
        }

        public IEnumerable<Message> GetMessages(int chatRoomId)
        {
            if (chatRoomId < 1)
            {
                throw new ArgumentException("Chat room Id must be greater than zero");
            }

            SortedDictionary<DateTime, Message> messages = GetMessagesDictionary(chatRoomId);
            return messages.Values;
        }

        public void AddMessage(MessageInput message)
        {
            this.ValidateInputMessage(message);

            SortedDictionary<DateTime, Message> messages = GetMessagesDictionary(message.ChatRoomId);
            Message newMessage = m_MessagerProvider.AddMessage(message);
            messages.Add(newMessage.Date, newMessage);
        }

        public void AddSystemMessage(ChatRoom chatRoom, SystemMessageType messageType)
        {
            DateTime? messageDate = null;
            string messageText = string.Empty;
            switch (messageType)
            {
                case SystemMessageType.Start:
                    messageDate = chatRoom.StartDate;
                    messageText = "--- Chat started ---";
                    break;
                case SystemMessageType.End:
                    messageDate = chatRoom.EndDate;
                    messageText = "--- Chat ended ---";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("System message type is not defined");
            }

            if (messageDate != null && !string.IsNullOrWhiteSpace(messageText))
            {
                Message systemMessage = new Message()
                {
                    Id = 0,
                    ChatRoomId = chatRoom.Id,
                    Date = messageDate.Value,
                    ParticipantName = string.Empty,
                    Content = messageText,
                    IsSystemMessage = true,
                };

                SortedDictionary<DateTime, Message> messages = this.GetMessagesDictionary(systemMessage.ChatRoomId);
                messages.Add(systemMessage.Date, systemMessage);
            }
        }

        private SortedDictionary<DateTime, Message> GetMessagesDictionary(int chatRoomId)
        {
            SortedDictionary<DateTime, Message> messages = m_MessagesByRoomId.GetOrAdd(chatRoomId, (id) =>
                                    {
                                        IEnumerable<Message> messagesInDb = m_MessagerProvider.GetMessagesByRoomId(chatRoomId);

                                        m_Logger.InfoFormat("Messages in chat room #{0} get from database", chatRoomId);

                                        return new SortedDictionary<DateTime, Message>(messagesInDb.ToDictionary(m => m.Date));
                                    });
            return messages;
        }

        private void ValidateInputMessage(MessageInput message)
        {
            if (message.ChatRoomId < 1)
            {
                throw new ArgumentException("Chat room Id must be greater than zero");
            }

            if (message.ParticipantId < 1)
            {
                throw new ArgumentException("Chat room Id must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(message.Content))
            {
                throw new ArgumentException("Message content cannot be null or space");
            }
        }
    }
}
