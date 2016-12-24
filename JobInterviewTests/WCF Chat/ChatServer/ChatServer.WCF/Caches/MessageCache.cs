namespace ChatServer.WCF.Caches
{
    using System.Collections.Generic;
    using ChatServer.Common.Cache;
    using ChatServer.Data.Interfaces;
    using ChatServer.Data.Model.MessageRepository;
    using ChatServer.WCF.Models;

    public class MessageCache : BaseCacheService<ICollection<Message>>, IMessageCache
    {
        private INaxexChatData data;

        public MessageCache(INaxexChatData data)
            : base("messages", 1)
        {
            this.data = data;
        }

        protected override ICollection<Message> GetItemsFromDataSource(int chatRoomId)
        {
            IEnumerable<MessageModel> messageModels = this.data.Messages.GetMessages(chatRoomId);

            ICollection<Message> messages = new List<Message>();
            foreach (MessageModel messageModel in messageModels)
            {
                messages.Add(Message.Map(messageModel));
            }

            return messages;
        }
    }
}