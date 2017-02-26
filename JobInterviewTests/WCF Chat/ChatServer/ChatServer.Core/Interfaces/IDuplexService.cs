using System.Collections.Generic;
using System.ServiceModel;
using ChatServer.Common.Models;

namespace ChatServer.Core.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IDuplexServiceCallBack))]
    public interface IDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void GetAllOnline(string userName);
    }
}
