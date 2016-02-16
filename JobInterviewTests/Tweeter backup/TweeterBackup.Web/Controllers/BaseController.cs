namespace TweeterBackup.Web.Controllers
{
    using System.Web.Mvc;

    using TweeterBackup.Data;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Extensions;
    using TweeterBackup.Web.Infrastructure.UserProvider;

    [Authorize]
    public abstract class BaseController : Controller
    {
        public BaseController(ITweeterBackupData backupData, ITwitter tweeter, IUserIdProvider userProvider)
        {
            this.BackupData = backupData;
            this.Twitter = tweeter;
            this.UserProvider = userProvider;
            this.CurrentUserId = this.UserProvider.GetUserId();
        }

        protected ITwitter Twitter { get; private set; }

        protected ITweeterBackupData BackupData { get; private set; }

        protected IUserIdProvider UserProvider { get; private set; }

        protected string CurrentUserId { get; private set; }

        protected ActionResult Notification(string message, MessageColourType type)
        {
            var messageObj = new NotificationMessage()
            {
                Message = message,
                ColourType = type
            };

            return this.PartialView("_NotificationPartial", messageObj);
        }
    }
}