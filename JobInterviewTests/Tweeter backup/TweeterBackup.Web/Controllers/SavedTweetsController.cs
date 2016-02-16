namespace TweeterBackup.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using TweeterBackup.Data;
    using TweeterBackup.Data.Common.Repository;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Extensions;
    using TweeterBackup.Web.Infrastructure.UserProvider;
    using TweeterBackup.Web.Models;

    public class SavedTweetsController : KendoBaseController<Tweet, SavedTweetsViewModel>
    {
        public SavedTweetsController(ITweeterBackupData backupData, ITwitter tweeter, IUserIdProvider userProvider)
            : base(backupData, tweeter, userProvider)
        {
        }

        // GET: SavedTweets
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Retweet(long? id)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                {
                    return this.Notification("Invalid request", MessageColourType.Error);
                }

                if (id == null)
                {
                    return this.Notification("Invalid Tweet Id", MessageColourType.Error);
                }

                var tweetInDb = this.BackupData.Tweets.All().FirstOrDefault(t => t.UserId == this.CurrentUserId && t.TwitterId == id.ToString());

                if (tweetInDb == null)
                {
                    return this.Notification("Invalid Tweet Id", MessageColourType.Error);
                }

                if (tweetInDb.IsReTweeted)
                {
                    return this.Notification("You have already retweeted that tweet", MessageColourType.Info);
                }

                var response = this.Twitter.RetweetById(id.Value);

                tweetInDb.IsReTweeted = true;
                this.BackupData.SaveChanges();

                return this.Notification("Tweet retweeted", MessageColourType.Success);
            }
            catch (WebException ex)
            {
                return this.Notification(string.Format("Unknown tweet id: {0}", ex.Message), MessageColourType.Error);
            }
        }

        protected override IRepository<Tweet> GetData()
        {
            return this.BackupData.Tweets;
        }

        protected override IQueryable<Tweet> GetAllData()
        {
            return this.GetData().All().Where(u => u.UserId == this.CurrentUserId);
        }

        protected override object ModelId(SavedTweetsViewModel model)
        {
            return model.Id;
        }
    }
}