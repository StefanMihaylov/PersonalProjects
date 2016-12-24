namespace ChatServer.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Data.Model.ParticipantRepository;

    public interface IChatRoomRepository
    {
        ChatRoomModel AddChatRoom(string userNameA, string userNameB);

        IEnumerable<ChatRoomModel> GetAll();

        ChatRoomModel GetRoomById(int id);
    }
}
