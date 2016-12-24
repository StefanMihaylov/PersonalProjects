namespace ChatServer.WCF.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;
    using ChatServer.Data.Model.MessageRepository;

    [DataContract]
    public class Message
    {
        public static Func<MessageModel, Message> Map
        {
            get
            {
                return message => new Message
                {
                    Id = message.Id,
                    ChatRoomId = message.ChatRoomId,
                    ParticipantName = message.ParticipantName,
                    Content = message.Message,
                    Date = message.Date,
                };
            }
        }

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
    }
}