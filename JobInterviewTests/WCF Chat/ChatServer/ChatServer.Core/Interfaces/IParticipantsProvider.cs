namespace ChatServer.Core.Interfaces
{
    using System.Collections.Generic;
    using ChatServer.Common.Models;

    public interface IParticipantsProvider
    {
        IEnumerable<Participant> GetAll();

        Participant Login(string userName);
    }
}