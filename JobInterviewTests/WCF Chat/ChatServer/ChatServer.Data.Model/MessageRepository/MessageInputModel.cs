namespace ChatServer.Data.Model.MessageRepository
{
    public class MessageInputModel
    {
        public int ChatRoomId { get; set; }

        public int ParticipantId { get; set; }

        public string Message { get; set; }
    }
}
