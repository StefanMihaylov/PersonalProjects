namespace TweeterBackup.Data.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TwitterId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public bool IsReTweeted { get; set; }

        public int AuthorId { get; set; }

        public virtual Favourite Author { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
