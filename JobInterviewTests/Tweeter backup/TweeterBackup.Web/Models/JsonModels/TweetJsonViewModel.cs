namespace TweeterBackup.Web.Models.JsonModels
{
    public class TweetJsonViewModel
    {
        public string Id_str { get; set; }

        public string Text { get; set; }

        public string Created_at { get; set; }

        public FavouriteJsonViewModel User { get; set; }
    }
}