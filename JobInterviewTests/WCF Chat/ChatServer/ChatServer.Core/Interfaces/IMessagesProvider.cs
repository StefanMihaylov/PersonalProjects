using System;
using System.Collections.Generic;
using ChatServer.Common.Models;

namespace ChatServer.Core.Interfaces
{
    public interface IMessagesProvider
    {
        Message AddMessage(MessageInput message);

        IEnumerable<Message> GetMessagesByRoomId(int chatRoomId);
    }
}