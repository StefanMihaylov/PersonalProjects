namespace ChatServer.Database
{
    using System;

    using DTO = ChatServer.Common.Models;

    public partial class ChatMessage
    {
        public static implicit operator DTO.Message(ChatMessage message)
        {
            if (message == null)
            {
                return null;
            }

            DTO.Message dtoMessage = new DTO.Message
            {
                Id = message.Id,
                ChatRoomId = message.ChatRoomId,
                ParticipantName = message.Participant.Username,
                Content = message.Message,
                Date = message.Date,
                IsSystemMessage = false,
            };

            return dtoMessage;
        }

        public static ChatMessage Create(DTO.MessageInput message)
        {
            return new ChatMessage()
            {
                ChatRoomId = message.ChatRoomId,
                ParticipantId = message.ParticipantId,
                Message = message.Content,
                Date = DateTime.Now,
            };
        }
    }
}
