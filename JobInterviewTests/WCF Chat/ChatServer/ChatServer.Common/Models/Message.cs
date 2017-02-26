namespace ChatServer.Common.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Message
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ChatRoomId { get; set; }

        [DataMember]
        public string ParticipantName { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public bool IsSystemMessage { get; set; }
    }
}
