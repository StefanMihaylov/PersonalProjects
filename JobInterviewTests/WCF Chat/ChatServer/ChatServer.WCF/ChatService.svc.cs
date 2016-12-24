namespace ChatServer.WCF
{
    using System.Collections.Generic;
    using ChatServer.Data;
    using ChatServer.Data.Interfaces;
    using ChatServer.Data.Model.MessageRepository;
    using ChatServer.Data.Model.ParticipantRepository;
    using ChatServer.WCF.Caches;
    using ChatServer.WCF.Models;

    public class ChatService : IChatService
    {
        public ChatService(INaxexChatData data, IMessageCache messageCache)
        {
            this.Data = data;
            this.MessageCache = messageCache;
        }

        public ChatService(INaxexChatData data)
            : this(data, new MessageCache(data))
        {
        }

        public ChatService()
            : this(new NaxexChatData())
        {
        }

        protected INaxexChatData Data { get; private set; }

        protected IMessageCache MessageCache { get; private set; }

        public Participant Login(string userName)
        {
            ParticipantModel participantModel = this.Data.Participants.Login(userName);
            Participant participant = Participant.Map(participantModel);
            return participant;
        }

        public IEnumerable<Participant> GetOtherPaticipants(string userName)
        {
            IEnumerable<ParticipantModel> participantModels = this.Data.Participants.GetAll(userName);

            ICollection<Participant> participants = new List<Participant>();
            foreach (ParticipantModel participantModel in participantModels)
            {
                participants.Add(Participant.Map(participantModel));
            }

            return participants;
        }

        public ChatRoom CreateChatRoom(string participantA, string participantB)
        {
            ChatRoomModel chatRoomModel = this.Data.ChatRooms.AddChatRoom(participantA, participantB);
            ChatRoom chatRoom = ChatRoom.Map(chatRoomModel);
            return chatRoom;
        }

        public void AddMessage(int chatRoomId, int participantId, string message)
        {
            this.Data.Messages.AddMessage(new MessageInputModel()
            {
                ChatRoomId = chatRoomId,
                ParticipantId = participantId,
                Message = message
            });
        }

        public IEnumerable<Message> GetAllMessages(int chatRoomId)
        {
            IEnumerable<Message> messages = this.MessageCache.GetAll(chatRoomId);
            return messages;
        }
    }
}
