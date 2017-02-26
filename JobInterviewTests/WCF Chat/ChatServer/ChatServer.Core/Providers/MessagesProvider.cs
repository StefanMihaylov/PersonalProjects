using System;
using System.Collections.Generic;
using ChatServer.Common.Models;
using ChatServer.Core.Interfaces;
using ChatServer.Data.Interfaces;

namespace ChatServer.Core.Providers
{
    public class MessagesProvider : IMessagesProvider
    {
        private readonly INaxexChatData m_NaxexChatData;

        public MessagesProvider(INaxexChatData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "NaxexChatData is null");
            }

            m_NaxexChatData = data;
        }

        public IEnumerable<Message> GetMessagesByRoomId(int chatRoomId)
        {
            IEnumerable<Message> messages = m_NaxexChatData.Messages.GetMessages(chatRoomId);
            return messages;
        }

        public Message AddMessage(MessageInput input)
        {
            Message message = m_NaxexChatData.Messages.AddMessage(input);
            return message;
        }
    }
}
