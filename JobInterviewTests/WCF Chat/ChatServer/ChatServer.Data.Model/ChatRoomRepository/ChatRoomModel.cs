namespace ChatServer.Data.Model.ParticipantRepository
{
    using System;
    using System.Linq.Expressions;
    using ChatServer.Database;

    public class ChatRoomModel
    {
        public static Expression<Func<ChatRoom, ChatRoomModel>> FromDb
        {
            get
            {
                return chatRoom => new ChatRoomModel
                {
                    Id = chatRoom.Id,
                    ParticipantAId = chatRoom.ParticipantAId,
                    ParticipantAName = chatRoom.Participant.Username,
                    ParticipantBId = chatRoom.Participant1.Id,
                    ParticipantBName = chatRoom.Participant1.Username,
                    StartDate = chatRoom.StartDate,
                    EndDate = chatRoom.EndDate,
                };
            }
        }

        public int Id { get; set; }

        public int ParticipantAId { get; set; }

        public string ParticipantAName { get; set; }

        public int ParticipantBId { get; set; }

        public string ParticipantBName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
