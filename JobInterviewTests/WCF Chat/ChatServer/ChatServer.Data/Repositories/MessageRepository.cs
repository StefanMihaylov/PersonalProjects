namespace ChatServer.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChatServer.Common.Base;
    using ChatServer.Data.Interfaces;
    using ChatServer.Database;
    using ChatServer.Database.Interfaces;
    using DTO = ChatServer.Common.Models;

    public class MessageRepository : GenericRepository<ChatMessage, INaxexChatDbContext>, IMessageRepository
    {
        public MessageRepository(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IEnumerable<DTO.Message> GetMessages(int chatRoomId)
        {
            if (chatRoomId < 1)
            {
                throw new ArgumentOutOfRangeException("chatRoomId");
            }

            List<DTO.Message> messages = this.All()
                                             .Where(m => m.ChatRoomId == chatRoomId)
                                             .AsEnumerable()
                                             .Select(m => (DTO.Message)m)
                                             .ToList();
            return messages;
        }

        public DTO.Message AddMessage(DTO.MessageInput message)
        {
            ChatMessage messageInDb = ChatMessage.Create(message);
            this.Add(messageInDb);
            this.Context.SaveChanges();

            return messageInDb;
        }
    }
}
