namespace ChatServer.Common.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Participant
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}