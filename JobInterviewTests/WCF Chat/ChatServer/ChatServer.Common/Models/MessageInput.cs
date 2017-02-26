namespace ChatServer.Common.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MessageInput
    {
        [DataMember]
        public int ChatRoomId { get; set; }

        [DataMember]
        public int ParticipantId { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}
