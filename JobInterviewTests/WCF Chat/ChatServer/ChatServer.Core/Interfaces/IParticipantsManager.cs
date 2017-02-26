namespace ChatServer.Core.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using ChatServer.Common.Models;

    [ServiceContract]
    public interface IParticipantsManager
    {
        [OperationContract]
        IEnumerable<Participant> GetAllOnline(string userName);

        [OperationContract]
        Participant Login(string userName);

        [OperationContract]
        void Logout(string userName);
    }
}