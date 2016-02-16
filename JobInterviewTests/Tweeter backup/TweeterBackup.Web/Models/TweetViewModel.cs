namespace TweeterBackup.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TweeterBackup.Data.Model;
    using TweeterBackup.Web.Infrastructure.Mapping;

    public class TweetViewModel : IMapFrom<Tweet>
    {
        public int Id { get; set; }

        [Required]
        public string TwitterId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Text { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime CreatedAt { get; set; }

        public long AuthorId { get; set; }
    }
}