namespace ChatServer.Database.Interfaces
{
    using System.Data.Entity;
    using ChatServer.Common.Interfaces;

    public interface INaxexChatDbContext : IDbContext
    {
        DbSet<ChatMessage> ChatMessages { get; set; }

        DbSet<ChatRoom> ChatRooms { get; set; }

        DbSet<Participant> Participants { get; set; }
    }
}
