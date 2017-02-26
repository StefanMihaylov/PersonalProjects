namespace ChatServer.Core.Managers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Activation;
    using ChatServer.Common.Models;
    using ChatServer.Core.Interfaces;

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ParticipantsManager : IParticipantsManager, IParticipantsManagerREST
    {
        private readonly IParticipantsProvider m_AccountsProvider;
        private readonly IChatRoomsManager m_ChatRoomManager;
        private readonly ConcurrentDictionary<string, Participant> m_OnlineParticipantsByName;

        public ParticipantsManager(IParticipantsProvider participantsProvider, IChatRoomsManager chatRoomManager)
        {
            if (participantsProvider == null)
            {
                throw new ArgumentNullException("participantsProvider", "ParticipantsProvider is null");
            }

            if (chatRoomManager == null)
            {
                throw new ArgumentNullException("chatRoomManager", "chatRoomManager is null");
            }

            m_AccountsProvider = participantsProvider;
            m_ChatRoomManager = chatRoomManager;
            m_OnlineParticipantsByName = new ConcurrentDictionary<string, Participant>();
        }

        public Participant Login(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName", "UserName cannot be null or space");
            }

            if (userName.Length <= 2 || userName.Length > 50)
            {
                throw new ArgumentException("Username lenght must be between 2 and 50 characters");
            }

            Participant result = m_OnlineParticipantsByName.GetOrAdd(userName,
                                   (providerUserName) =>
                                       {
                                           Participant participant = m_AccountsProvider.Login(providerUserName);
                                           if (participant == null)
                                           {
                                               throw new ApplicationException("Cannot login participant from AccountsProvider. Username: " + providerUserName);
                                           }

                                           return participant;
                                       }
                                  );

            return result;
        }

        public void Logout(string userName)
        {
            Participant logoutParticipant;
            if (m_OnlineParticipantsByName.TryRemove(userName, out logoutParticipant))
            {
                m_ChatRoomManager.CloseChatRoomsByUserName(userName);
            }
        }

        public IEnumerable<Participant> GetAllOnline(string userName)
        {
            IEnumerable<Participant> participants = m_OnlineParticipantsByName.Values.Where(p => p.Username != userName);
            return participants;
        }
    }
}
