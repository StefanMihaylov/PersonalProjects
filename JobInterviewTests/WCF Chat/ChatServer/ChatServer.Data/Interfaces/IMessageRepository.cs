namespace ChatServer.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Data.Model.MessageRepository;

    public interface IMessageRepository
    {
        void AddMessage(MessageInputModel message);

        IEnumerable<MessageModel> GetMessages(int chatRoomId);
    }
}
