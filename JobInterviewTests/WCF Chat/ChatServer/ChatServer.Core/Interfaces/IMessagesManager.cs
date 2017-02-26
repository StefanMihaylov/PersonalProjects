using System;
using System.Collections.Generic;
using System.ServiceModel;
using ChatServer.Common.Models;

namespace ChatServer.Core.Interfaces
{
    [ServiceContract]
    public interface IMessagesManager
    {
        [OperationContract]
        void AddMessage(MessageInput message);

        [OperationContract]
        IEnumerable<Message> GetMessages(int chatRoomId);

        void AddSystemMessage(ChatRoom chatRoom, SystemMessageType messageType);
    }
}