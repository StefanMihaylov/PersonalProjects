namespace ChatServer.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Common.Interfaces;
    using ChatServer.Data.Model.ParticipantRepository;
    using ChatServer.Database;

    public interface IParticipantRepository : IRepository<Participant>
    {
        ParticipantModel Login(string userName);

        IEnumerable<ParticipantModel> GetAll();

        IEnumerable<ParticipantModel> GetAll(string userName);

        ParticipantModel GetParticipantById(int id);

        ParticipantModel GetParticipantByName(string userName);
    }
}
