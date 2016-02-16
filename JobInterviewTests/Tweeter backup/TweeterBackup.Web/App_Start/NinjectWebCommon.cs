[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TweeterBackup.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TweeterBackup.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TweeterBackup.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using TweeterBackup.Common;
    using TweeterBackup.Data;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Infrastructure.UserProvider;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAppDbContext>().To<AppDbContext>();
            kernel.Bind<ITweeterBackupData>().To<TweeterBackupData>();
            kernel.Bind<IUserIdProvider>().To<UserIdProvider>();
            kernel.Bind<ITwitter>().To<Twitter>()
                                   .WithConstructorArgument("consumerKey", GlobalConstants.TwitterConsumerKey)
                                   .WithConstructorArgument("consumerKeySecret", GlobalConstants.TwitterConsumerSecret)
                                   .WithConstructorArgument("accessToken", GlobalConstants.TwitterAccessToken)
                                   .WithConstructorArgument("accessTokenSecret", GlobalConstants.TwitterAccessSecret);
        }
    }
}
