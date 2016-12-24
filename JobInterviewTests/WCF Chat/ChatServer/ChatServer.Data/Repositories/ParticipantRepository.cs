namespace ChatServer.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using ChatServer.Common.Base;
    using ChatServer.Data.Interfaces;
    using ChatServer.Data.Model.ParticipantRepository;
    using ChatServer.Database;
    using ChatServer.Database.Interfaces;

    public class ParticipantRepository : GenericRepository<Participant, INaxexChatDbContext>, IParticipantRepository
    {
        public ParticipantRepository(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ParticipantModel> GetAll()
        {
            IEnumerable<ParticipantModel> participants = this.All()
                                                             .Select(ParticipantModel.FromDb)
                                                             .ToList();
            return participants;
        }

        public IEnumerable<ParticipantModel> GetAll(string userName)
        {
            IEnumerable<ParticipantModel> participants = this.All()
                                                             .Where(p => p.Username != userName)
                                                             .Select(ParticipantModel.FromDb)
                                                             .ToList();
            return participants;
        }

        public ParticipantModel GetParticipantById(int id)
        {
            ParticipantModel participant = this.All()
                                          .Where(p => p.Id == id)
                                          .Select(ParticipantModel.FromDb)
                                          .FirstOrDefault();
            return participant;
        }

        public ParticipantModel GetParticipantByName(string userName)
        {
            ParticipantModel participant = this.All()
                                          .Where(p => p.Username == userName)
                                          .Select(ParticipantModel.FromDb)
                                          .FirstOrDefault();
            return participant;
        }

        public ParticipantModel Login(string userName)
        {
            ParticipantModel participant = this.GetParticipantByName(userName);
            if (participant == null)
            {
                this.Add(new Participant()
                {
                    Username = userName,
                });

                this.Context.SaveChanges();

                participant = this.GetParticipantByName(userName);
            }

            return participant;
        }
    }
}
