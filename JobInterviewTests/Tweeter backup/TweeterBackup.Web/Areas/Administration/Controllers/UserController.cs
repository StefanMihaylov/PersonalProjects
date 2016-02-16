namespace TweeterBackup.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using TweeterBackup.Common;
    using TweeterBackup.Data;
    using TweeterBackup.Data.Common.Repository;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Areas.Administration.Models;
    using TweeterBackup.Web.Controllers;
    using TweeterBackup.Web.Infrastructure.UserProvider;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class UserController : KendoBaseController<User, UserViewModel>
    {
        public UserController(ITweeterBackupData backupData, ITwitter tweeter, IUserIdProvider userProvider)
            : base(backupData, tweeter, userProvider)
        {
        }

        // GET: Administration/User
        public ActionResult Index()
        {
            return this.View();
        }

        protected override IRepository<User> GetData()
        {
            return this.BackupData.Users;
        }

        protected override IQueryable<User> GetAllData()
        {
            return this.GetData().All();
        }

        protected override object ModelId(UserViewModel model)
        {
            return model.Id;
        }
    }
}