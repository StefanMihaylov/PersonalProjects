using System.Collections.Generic;
using System.ServiceModel;
using ChatServer.Common.Models;

namespace ChatServer.Core.Interfaces
{
    [ServiceContract]
    public interface IChatRoomsManager
    {
        [OperationContract]
        ChatRoom OpenChatRoom(IEnumerable<string> participantNames);

        [OperationContract]
        ChatRoom OpenChatRoomById(int chatRoomId);

        [OperationContract]
        IEnumerable<ChatRoom> GetAllOpenChatRooms(string username);

        [OperationContract]
        void CloseChatRoomById(int chatRoomId);

        void CloseChatRoomsByUserName(string username);
    }
}