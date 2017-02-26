using System;
using System.IO;
using System.ServiceModel;

using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

//using TradeNetworks.Common.Communication.Localization;
//using TradeNetworks.Servers.SessionServer.AuthenticationLayer;
using log4net.Config;

namespace ChatServer.WCF
{
    public class Global : System.Web.HttpApplication
    {
        private ILogger m_Logger;

        private IWindsorContainer m_WindsorContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            FileInfo fileInfo = new FileInfo(string.Format("{0}\\log4Net.config", AppDomain.CurrentDomain.BaseDirectory));
            XmlConfigurator.ConfigureAndWatch(fileInfo);

            m_WindsorContainer = new WindsorContainer(new XmlInterpreter());
            // DefaultServiceHostFactory.RegisterContainer(m_WindsorContainer.Kernel);

            m_Logger = m_WindsorContainer.Resolve<ILogger>();
            m_Logger.Info("Chat Server application starting..");

            //WCFAuthorizationManager.RegisterContainer(m_WindsorContainer.Kernel);
            //HttpAuthorizationManager.RegisterContainer(m_WindsorContainer.Kernel);
            //LocalizationEndpointBehavior.RegisterContainer(m_WindsorContainer.Kernel);

            m_Logger.Info("Chat Server application started successfully.");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            m_WindsorContainer.Dispose();
            m_Logger.Info("Chat server stopped successfully.");
        }
    }
}