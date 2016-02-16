namespace TweeterBackup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TweeterBackup.Data.Migrations;
    using TweeterBackup.Data.Model;

    public class AppDbContext : IdentityDbContext<User>, IAppDbContext
    {
        public AppDbContext()
            : base("TwitterBackupDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        public virtual IDbSet<Favourite> Favourites { get; set; }

        public virtual IDbSet<Tweet> Tweets { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
