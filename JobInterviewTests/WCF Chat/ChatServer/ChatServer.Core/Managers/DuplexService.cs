using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using ChatServer.Common.Models;
using ChatServer.Core.Interfaces;

namespace ChatServer.Core.Managers
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DuplexService : IDuplexService
    {
        private readonly IParticipantsManager m_ParticipantManager;
        private readonly ILogger m_logger;

        public DuplexService(IParticipantsManager participantManager, ILogger logger)
        {
            m_ParticipantManager = participantManager;
            m_logger = logger;
        }

        public void GetAllOnline(string userName)
        {
            m_logger.InfoFormat("Session Id: {0}, Caller sent: {1}", userName, OperationContext.Current.SessionId);

            var users = m_ParticipantManager.GetAllOnline(userName);
            var callbackInstance = OperationContext.Current.GetCallbackChannel<IDuplexServiceCallBack>();
            callbackInstance.GetAllOnlineCallBack(users);
        }
    }
}
