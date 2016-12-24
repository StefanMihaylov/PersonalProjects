namespace ChatServer.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChatServer.Common.Base;
    using ChatServer.Data.Interfaces;
    using ChatServer.Data.Model.ParticipantRepository;
    using ChatServer.Database;
    using ChatServer.Database.Interfaces;

    public class ChatRoomRepository : GenericRepository<ChatRoom, INaxexChatDbContext>, IChatRoomRepository
    {
        public ChatRoomRepository(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ChatRoomModel> GetAll()
        {
            IEnumerable<ChatRoomModel> chatRooms = this.All()
                                                       .Select(ChatRoomModel.FromDb)
                                                       .ToList();
            return chatRooms;
        }

        public ChatRoomModel GetRoomById(int id)
        {
            ChatRoomModel chatRoom = this.All()
                                         .Where(c => c.Id == id)
                                         .Select(ChatRoomModel.FromDb)
                                         .FirstOrDefault();
            return chatRoom;
        }

        public ChatRoomModel AddChatRoom(string userNameA, string userNameB)
        {
            ChatRoomModel existingChatRoom = this.All()
                                            .Where(c => (c.Participant.Username == userNameA && c.Participant1.Username == userNameB) ||
                                                        (c.Participant.Username == userNameB && c.Participant1.Username == userNameA))
                                            .Select(ChatRoomModel.FromDb)
                                            .FirstOrDefault();
            if (existingChatRoom != null)
            {
                if (existingChatRoom.ParticipantAName != userNameA)
                {
                    string firstUserName = existingChatRoom.ParticipantAName;
                    existingChatRoom.ParticipantAName = existingChatRoom.ParticipantBName;
                    existingChatRoom.ParticipantBName = firstUserName;

                    int firstId = existingChatRoom.ParticipantAId;
                    existingChatRoom.ParticipantAId = existingChatRoom.ParticipantBId;
                    existingChatRoom.ParticipantBId = firstId;
                }

                return existingChatRoom;
            }

            Participant participantA = this.GetParticipant(userNameA);
            Participant participantB = this.GetParticipant(userNameB);

            ChatRoom chatRoomInDb = new ChatRoom()
            {
                Participant = participantA,
                Participant1 = participantB,
                StartDate = DateTime.Now,
                EndDate = null,
            };

            this.Add(chatRoomInDb);
            this.Context.SaveChanges();

            ChatRoomModel chatRoom = ChatRoomModel.FromDb.Compile()(chatRoomInDb);
            return chatRoom;
        }

        private Participant GetParticipant(string username)
        {
            Participant participantA = this.Context.Participants.Where(p => p.Username == username)
                                                                .FirstOrDefault();
            if (participantA == null)
            {
                participantA = new Participant()
                {
                    Username = username
                };
            }

            return participantA;
        }
    }
}
