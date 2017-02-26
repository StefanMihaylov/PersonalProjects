namespace ChatServer.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Common.Models;

    public interface IChatRoomRepository
    {
        ChatRoom GetChatRoom(IEnumerable<string> userNames);

        IEnumerable<ChatRoom> GetAll();

        ChatRoom GetRoomById(int id);

        ChatRoom UpdateStartDate(int chatRoomId);

        ChatRoom UpdateEndDate(int chatRoomId);
    }
}
