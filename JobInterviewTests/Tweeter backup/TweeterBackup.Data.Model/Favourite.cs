namespace TweeterBackup.Data.Model
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Favourite
    {
        private ICollection<Tweet> tweets;

        private ICollection<User> users;

        public Favourite()
        {
            this.tweets = new HashSet<Tweet>();
            this.users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public long TwitterId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(20, MinimumLength = 3)] // According to Twitter documentation maximum 20 characters are allowed
        public string Name { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Tweet> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
