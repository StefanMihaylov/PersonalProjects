namespace ChatServer.Data.Model.MessageRepository
{
    using System;
    using System.Linq.Expressions;
    using ChatServer.Database;

    public class MessageModel
    {
        public static Expression<Func<ChatMessage, MessageModel>> FromDb
        {
            get
            {
                return message => new MessageModel
                {
                    Id = message.Id,
                    ChatRoomId = message.ChatRoomId,
                    ParticipantName = message.Participant.Username,
                    Message = message.Message,
                    Date = message.Date,
                };
            }
        }

        public int Id { get; set; }

        public int ChatRoomId { get; set; }

        public string ParticipantName { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
