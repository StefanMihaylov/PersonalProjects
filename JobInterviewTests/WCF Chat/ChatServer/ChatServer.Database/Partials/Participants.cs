namespace ChatServer.Database
{
    using DTO = ChatServer.Common.Models;
    public partial class Participant
    {
        public static implicit operator DTO.Participant(Participant participant)
        {
            if (participant == null)
            {
                return null;
            }

            DTO.Participant dtoParticipant = new DTO.Participant()
            {
                Id = participant.Id,
                Username = participant.Username,
            };

            return dtoParticipant;
        }

        public static Participant Create(DTO.Participant participant)
        {
            return new Participant()
            {
                Username = participant.Username,
            };
        }

        public static Participant Create(string username)
        {
            DTO.Participant participant = new DTO.Participant()
            {
                Username = username,
            };

            return Create(participant);
        }
    }
}
