[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(TweeterBackup.Web.Startup))]

namespace TweeterBackup.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
