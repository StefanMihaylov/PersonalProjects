namespace ChatServer.Data.Model.ParticipantRepository
{
    using System;
    using System.Linq.Expressions;
    using ChatServer.Database;

    public class ParticipantModel
    {
        public static Expression<Func<Participant, ParticipantModel>> FromDb
        {
            get
            {
                return participant => new ParticipantModel
                {
                    Id = participant.Id,
                    Username = participant.Username,
                };
            }
        }

        public int Id { get; set; }

        public string Username { get; set; }
    }
}
