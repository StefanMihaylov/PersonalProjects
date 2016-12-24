namespace ChatServer.WCF.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using ChatServer.Data.Model.ParticipantRepository;

    [DataContract]
    public class Participant
    {
        public static Func<ParticipantModel, Participant> Map
        {
            get
            {
                return participant => new Participant
                {
                    Id = participant.Id,
                    Username = participant.Username,
                };
            }
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}
