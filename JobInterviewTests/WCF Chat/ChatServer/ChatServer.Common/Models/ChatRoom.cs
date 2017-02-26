namespace ChatServer.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ChatRoom
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public IEnumerable<string> ParticipantNames { get; set; }

        [DataMember(Order = 3)]
        public DateTime StartDate { get; set; }

        [DataMember(Order = 4)]
        public DateTime? EndDate { get; set; }
    }
}
