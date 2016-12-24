namespace ChatServer.WCF.Caches
{
    using System.Collections.Generic;
    using ChatServer.WCF.Models;

    public interface IMessageCache
    {
        ICollection<Message> GetAll(int chatRoomId);

        void Clear(int chatRoomId);
    }
}