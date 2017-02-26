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

    public class ChatRoomRepository : GenericRepository<ChatRoom, INaxexChatDbContext>, IChatRoomRepository
    {
        public ChatRoomRepository(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IEnumerable<DTO.ChatRoom> GetAll()
        {
            IEnumerable<DTO.ChatRoom> chatRooms = this.All()
                                                       .Select(c => (DTO.ChatRoom)c)
                                                       .ToList();

            return chatRooms;
        }

        public DTO.ChatRoom GetRoomById(int id)
        {
            DTO.ChatRoom chatRoom = this.All()
                                         .Where(c => c.Id == id)
                                         .FirstOrDefault();
            return chatRoom;
        }

        public DTO.ChatRoom GetChatRoom(IEnumerable<string> userNames)
        {
            DTO.ChatRoom existingChatRoom = this.All()
                                            .Where(c => c.Participants.Select(p => p.Username).All(p => userNames.Contains(p)))
                                            .FirstOrDefault();
            if (existingChatRoom != null)
            {
                return existingChatRoom;
            }
            else
            {
                ChatRoom chatRoomInDb = new ChatRoom()
                {
                    StartDate = DateTime.Now,
                    EndDate = null,
                };

                foreach (var userName in userNames)
                {
                    Participant participant = this.GetParticipant(userName);
                    chatRoomInDb.Participants.Add(participant);
                }

                this.Add(chatRoomInDb);
                this.Context.SaveChanges();

                ChatRoom chatRoom = chatRoomInDb;
                return chatRoom;
            }
        }

        public DTO.ChatRoom UpdateStartDate(int chatRoomId)
        {
            ChatRoom chatRoom = this.GetChatRoomById(chatRoomId);

            chatRoom.StartDate = DateTime.Now;
            chatRoom.EndDate = null;
            this.Context.SaveChanges();

            return chatRoom;
        }

        public DTO.ChatRoom UpdateEndDate(int chatRoomId)
        {
            ChatRoom chatRoom = this.GetChatRoomById(chatRoomId);

            chatRoom.EndDate = DateTime.Now;
            this.Context.SaveChanges();

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

        private ChatRoom GetChatRoomById(int chatRoomId)
        {
            ChatRoom chatRoom = this.GetById(chatRoomId);
            if (chatRoom == null)
            {
                throw new ArgumentException(string.Format("Chat room #{0} not found in Db", chatRoomId));
            }

            return chatRoom;
        }
    }
}
