namespace ChatServer.Data
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Common.Base;
    using ChatServer.Common.Interfaces;
    using ChatServer.Data.Interfaces;
    using ChatServer.Data.Repositories;
    using ChatServer.Database;
    using ChatServer.Database.Interfaces;

    public class NaxexChatData : BaseUnitOfWork<INaxexChatDbContext>, INaxexChatData
    {
        public NaxexChatData(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IParticipantRepository Participants
        {
            get { return (ParticipantRepository)this.GetRepository<Participant>(); }
        }

        public IChatRoomRepository ChatRooms
        {
            get { return (ChatRoomRepository)this.GetRepository<ChatRoom>(); }
        }

        public IMessageRepository Messages
        {
            get { return (MessageRepository)this.GetRepository<ChatMessage>(); }
        }

        protected override IRepository<T> GetRepository<T>()
        {
            Type type = typeof(T);
            if (!this.ContainsRepository(type))
            {
                var dictionary = new Dictionary<Type, Type>()
                {
                    { typeof(Participant), typeof(ParticipantRepository) },
                    { typeof(ChatRoom), typeof(ChatRoomRepository) },
                    { typeof(ChatMessage), typeof(MessageRepository) },
                };

                foreach (Type modelType in dictionary.Keys)
                {
                    if (type.IsAssignableFrom(modelType))
                    {
                        Type repositoryType = dictionary[modelType];
                        this.AddRepository(type, repositoryType);
                        break;
                    }
                }
            }

            return base.GetRepository<T>();
        }
    }
}
