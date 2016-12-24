namespace ChatServer.WCF.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;
    using ChatServer.Data.Model.ParticipantRepository;

    [DataContract]
    public class ChatRoom
    {
        public static Func<ChatRoomModel, ChatRoom> Map
        {
            get
            {
                return chatRoom => new ChatRoom
                {
                    Id = chatRoom.Id,
                    ParticipantAId = chatRoom.ParticipantAId,
                    ParticipantAName = chatRoom.ParticipantAName,
                    ParticipantBName = chatRoom.ParticipantBName,
                    StartDate = chatRoom.StartDate,
                    EndDate = chatRoom.EndDate,
                };
            }
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ParticipantAId { get; set; }

        [DataMember]
        public string ParticipantAName { get; set; }

        [DataMember]
        public string ParticipantBName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }
    }
}