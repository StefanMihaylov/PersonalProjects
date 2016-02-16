namespace TweeterBackup.Web.Models
{
    using System.ComponentModel;

    public class TweetInputViewModel
    {
        [DisplayName(" ")]
        public string Id_str { get; set; }

        public string Text { get; set; }

        public string Created_at { get; set; }
    }
}