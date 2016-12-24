namespace ChatServer.WCF
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using ChatServer.WCF.Models;

    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        Participant Login(string userName);

        [OperationContract]
        IEnumerable<Participant> GetOtherPaticipants(string userName);

        [OperationContract]
        ChatRoom CreateChatRoom(string participantA, string participantB);

        [OperationContract]
        void AddMessage(int chatRoomId, int participantId, string message);

        [OperationContract]
        IEnumerable<Message> GetAllMessages(int chatRoomId);
    }
}
