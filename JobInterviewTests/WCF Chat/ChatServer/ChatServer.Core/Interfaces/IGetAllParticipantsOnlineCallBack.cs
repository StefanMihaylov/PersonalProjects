using System.Collections.Generic;
using System.ServiceModel;
using ChatServer.Common.Models;

namespace ChatServer.Core.Interfaces
{
    public interface IDuplexServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void GetAllOnlineCallBack(IEnumerable<Participant> users);
    }
}
