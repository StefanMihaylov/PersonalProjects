namespace ChatServer.Core.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using ChatServer.Common.Models;

    [ServiceContract]
    public interface IParticipantsManagerREST
    {
        [OperationContract(Name = "GetAllOnlineREST")]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/GetAllOnline/{userName}")]
        IEnumerable<Participant> GetAllOnline(string userName);

        [OperationContract(Name = "LoginREST")]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/Login/{userName}")]
        Participant Login(string userName);

        [OperationContract(Name = "LogoutREST")]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/Logout/{userName}")]
        void Logout(string userName);
    }
}