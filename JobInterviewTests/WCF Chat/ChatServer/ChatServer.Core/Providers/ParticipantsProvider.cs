using System;
using System.Collections.Generic;
using ChatServer.Common.Models;
using ChatServer.Core.Interfaces;
using ChatServer.Data.Interfaces;

namespace ChatServer.Core.Providers
{
    public class ParticipantsProvider : IParticipantsProvider
    {
        private readonly INaxexChatData m_NaxexChatData;

        public ParticipantsProvider(INaxexChatData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "NaxexChatData is null");
            }

            m_NaxexChatData = data;
        }

        public Participant Login(string userName)
        {
            Participant participant = m_NaxexChatData.Participants.Login(userName);
            return participant;
        }

        public IEnumerable<Participant> GetAll()
        {
            IEnumerable<Participant> participants = m_NaxexChatData.Participants.GetAll();
            return participants;
        }
    }
}
