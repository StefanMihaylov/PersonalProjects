namespace TweeterBackup.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Newtonsoft.Json;

    using TweeterBackup.Data;
    using TweeterBackup.Data.Common.Repository;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Infrastructure.UserProvider;
    using TweeterBackup.Web.Models;
    using TweeterBackup.Web.Models.JsonModels;

    public class FavouritesController : KendoBaseController<Favourite, FavouriteViewModel>
    {
        public FavouritesController(ITweeterBackupData backupData, ITwitter tweeter, IUserIdProvider userProvider)
            : base(backupData, tweeter, userProvider)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, FavouriteViewModel newModel)
        {
            if (newModel != null && this.ModelState.IsValid)
            {
                var databaseModel = this.GetTwitterData(newModel);
                if (databaseModel == null)
                {
                    ModelState.AddModelError(string.Empty, string.Format("Invalid Twitter username \"{0}\"", newModel.Name));
                }
                else
                {
                    this.BackupData.SaveChanges();

                    newModel.Id = databaseModel.Id;
                    newModel.TwitterId = databaseModel.TwitterId;
                    newModel.Name = databaseModel.Name;
                    newModel.Image = databaseModel.Image;
                }
            }

            return this.JsonKendoResult(newModel, request);
        }

        protected override IQueryable<Favourite> GetAllData()
        {
            return this.GetData().All().Where(f => f.Users.Any(u => u.Id == this.CurrentUserId));
        }

        protected override IRepository<Favourite> GetData()
        {
            return this.BackupData.Favourites;
        }

        protected override object ModelId(FavouriteViewModel model)
        {
            return model.Id;
        }

        private Favourite GetTwitterData(FavouriteViewModel newModel)
        {
            try
            {
                var databaseModel = this.GetData().All().FirstOrDefault(c => c.Name == newModel.Name);
                if (databaseModel == null)
                {
                    var response = this.Twitter.GetUserInfo(newModel.Name);
                    var tweeterResult = JsonConvert.DeserializeObject<FavouriteJsonViewModel>(response);
                    databaseModel = new Favourite()
                    {
                        TwitterId = tweeterResult.Id,
                        Name = tweeterResult.Screen_Name,
                        Image = tweeterResult.Profile_image_url
                    };

                    this.GetData().Add(databaseModel);
                }

                var currentUser = this.BackupData.Users.GetById(this.CurrentUserId);
                databaseModel.Users.Add(currentUser);

                return databaseModel;
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}