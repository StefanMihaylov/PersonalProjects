namespace TweeterBackup.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Newtonsoft.Json;

    using TweeterBackup.Data;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Extensions;
    using TweeterBackup.Web.Infrastructure.UserProvider;
    using TweeterBackup.Web.Models;
    using TweeterBackup.Web.Models.JsonModels;

    public class TweetsController : BaseController
    {
        public const string TwitterDateTemplate = "ddd MMM dd HH:mm:ss +ffff yyyy";

        public TweetsController(ITweeterBackupData backupData, ITwitter tweeter, IUserIdProvider userProvider)
            : base(backupData, tweeter, userProvider)
        {
        }

        // GET: Tweets
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read(string id, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();
            if (id != null)
            {
                var response = this.Twitter.GetTweets(id, 100);
                var responseAsCollection = JsonConvert.DeserializeObject<IList<TweetInputViewModel>>(response);
                result = responseAsCollection.ToDataSourceResult(request);
            }

            return this.Json(result);
        }

        public ActionResult Save(long? id)
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

                var isTweetInDb = this.BackupData.Tweets.All().Where(t => t.UserId == this.CurrentUserId)
                                                              .Any(t => t.TwitterId == id.ToString());
                if (isTweetInDb)
                {
                    return this.Notification("You have already saved that tweet", MessageColourType.Info);
                }

                var response = this.Twitter.GetTweetById(id.Value);
                var tweeterResult = JsonConvert.DeserializeObject<TweetJsonViewModel>(response);
                var authorInDb = this.BackupData.Favourites.All()
                                                           .Where(a => a.TwitterId == tweeterResult.User.Id)
                                                           .FirstOrDefault();
                var tweet = new Tweet()
                {
                    Text = tweeterResult.Text,
                    TwitterId = tweeterResult.Id_str,
                    CreatedAt = DateTime.ParseExact(tweeterResult.Created_at, TwitterDateTemplate, new CultureInfo("en-US")),
                    IsReTweeted = false,
                    UserId = this.CurrentUserId,
                    Author = authorInDb
                };

                this.BackupData.Tweets.Add(tweet);
                this.BackupData.SaveChanges();

                return this.Notification("Tweet saved successfully", MessageColourType.Success);
            }
            catch (WebException ex)
            {
                return this.Notification(string.Format("Unknown tweet id: {0}", ex.Message), MessageColourType.Error);
            }
        }
    }
}