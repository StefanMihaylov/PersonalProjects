namespace TweeterBackup.Data
{
    using System.Data.Entity;
    using TweeterBackup.Data.Model;

    public interface IAppDbContext
    {
        IDbSet<Favourite> Favourites { get; set; }

        IDbSet<Tweet> Tweets { get; set; }

        int SaveChanges();
    }
}
