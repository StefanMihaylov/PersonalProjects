using System.Collections.Generic;
using ChatServer.Common.Models;

namespace ChatServer.Core.Interfaces
{
    public interface IChatRoomsProvider
    {
        ChatRoom GetById(int chatRoomId);

        ChatRoom OpenChatRoom(IEnumerable<string> participantNames);

        ChatRoom UpdateStartDate(int chatRoomId);

        ChatRoom UpdateEndDate(int chatRoomId);
    }
}