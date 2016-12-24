namespace ChatServer.Data.Interfaces
{
    using ChatServer.Common.Interfaces;
    using ChatServer.Database.Interfaces;

    public interface INaxexChatData : IUnitOfWork<INaxexChatDbContext>
    {
        IChatRoomRepository ChatRooms { get; }

        IMessageRepository Messages { get; }

        IParticipantRepository Participants { get; }
    }
}
