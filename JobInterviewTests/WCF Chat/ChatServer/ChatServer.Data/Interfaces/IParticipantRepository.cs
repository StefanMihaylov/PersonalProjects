namespace ChatServer.Data.Interfaces
{
    using System.Collections.Generic;
    using ChatServer.Common.Models;

    public interface IParticipantRepository //: IRepository<Database.Participant>
    {
        Participant Login(string userName);

        IEnumerable<Participant> GetAll();

        IEnumerable<Participant> GetAll(string userName);

        Participant GetParticipantById(int id);

        Participant GetParticipantByName(string userName);
    }
}
