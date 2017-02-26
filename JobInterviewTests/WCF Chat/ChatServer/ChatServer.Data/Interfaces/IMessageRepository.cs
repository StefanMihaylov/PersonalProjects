namespace ChatServer.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ChatServer.Common.Models;

    public interface IMessageRepository
    {
        Message AddMessage(MessageInput message);

        IEnumerable<Message> GetMessages(int chatRoomId);
    }
}
