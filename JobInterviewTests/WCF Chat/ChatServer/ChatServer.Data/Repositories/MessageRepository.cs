namespace ChatServer.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChatServer.Common.Base;
    using ChatServer.Data.Interfaces;
    using ChatServer.Data.Model.MessageRepository;
    using ChatServer.Database;
    using ChatServer.Database.Interfaces;

    public class MessageRepository : GenericRepository<ChatMessage, INaxexChatDbContext>, IMessageRepository
    {
        public MessageRepository(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IEnumerable<MessageModel> GetMessages(int chatRoomId)
        {
            IEnumerable<MessageModel> messages = this.All()
                                                     .Where(m => m.ChatRoomId == chatRoomId)
                                                     .OrderByDescending(m => m.Date)
                                                     .Take(100)
                                                     .OrderBy(m => m.Date)
                                                     .Select(MessageModel.FromDb)
                                                     .ToList();
            return messages;
        }

        public void AddMessage(MessageInputModel message)
        {
            this.Add(new ChatMessage()
            {
                ChatRoomId = message.ChatRoomId,
                ParticipantId = message.ParticipantId,
                Message = message.Message,
                Date = DateTime.Now,
            });

            this.Context.SaveChanges();
        }
    }
}
