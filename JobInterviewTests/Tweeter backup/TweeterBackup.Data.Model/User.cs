namespace TweeterBackup.Data.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Favourite> favourites;
        private ICollection<Tweet> tweets;

        public User()
        {
            this.favourites = new HashSet<Favourite>();
            this.tweets = new HashSet<Tweet>();
        }

        [Required]
        public string Screen_name { get; set; }

        public virtual ICollection<Favourite> Favourites
        {
            get { return this.favourites; }
            set { this.favourites = value; }
        }

        public virtual ICollection<Tweet> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
