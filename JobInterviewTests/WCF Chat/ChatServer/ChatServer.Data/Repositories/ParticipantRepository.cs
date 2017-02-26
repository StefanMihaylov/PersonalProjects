namespace ChatServer.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using ChatServer.Common.Base;
    using ChatServer.Data.Interfaces;
    using ChatServer.Database;
    using ChatServer.Database.Interfaces;
    using DTO = ChatServer.Common.Models;

    public class ParticipantRepository : GenericRepository<Participant, INaxexChatDbContext>, IParticipantRepository
    {
        public ParticipantRepository(INaxexChatDbContext context)
            : base(context)
        {
        }

        public IEnumerable<DTO.Participant> GetAll()
        {
            List<DTO.Participant> participantsDTO = this.All()
                                                        .Select(p => (DTO.Participant)p)
                                                        .ToList();
            return participantsDTO;
        }

        public IEnumerable<DTO.Participant> GetAll(string userName)
        {
            var participantsDTO = this.All()
                .Where(p => p.Username != userName)
                .Select(p => (DTO.Participant)p)
                .ToList();

            return participantsDTO;
        }

        public DTO.Participant GetParticipantById(int id)
        {
            Participant participant = this.All()
                                          .Where(p => p.Id == id)
                                          .FirstOrDefault();
            return participant;
        }

        public DTO.Participant GetParticipantByName(string userName)
        {
            DTO.Participant participant = this.All()
                                          .Where(p => p.Username == userName)
                                          .FirstOrDefault();
            return participant;
        }

        public DTO.Participant Login(string userName)
        {
            DTO.Participant participant = this.GetParticipantByName(userName);
            if (participant == null)
            {
                Participant dalParticipant = Participant.Create(userName);
                this.Add(dalParticipant);
                this.Context.SaveChanges();

                participant = dalParticipant;
            }

            return participant;
        }
    }
}
