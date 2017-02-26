namespace ChatServer.Database
{
    using System.Linq;
    using DTO = ChatServer.Common.Models;

    public partial class ChatRoom
    {
        public static implicit operator DTO.ChatRoom(ChatRoom chatRoom)
        {
            if (chatRoom == null)
            {
                return null;
            }

            DTO.ChatRoom dtoChatRoom = new DTO.ChatRoom()
            {
                Id = chatRoom.Id,
                StartDate = chatRoom.StartDate,
                EndDate = chatRoom.EndDate,
                ParticipantNames = chatRoom.Participants.Select(p => p.Username).ToList(),
            };

            return dtoChatRoom;
        }
    }
}
