namespace TweeterBackup.Data
{
    using System;
    using TweeterBackup.Data.Common.Repository;
    using TweeterBackup.Data.Model;

    public interface ITweeterBackupData
    {
        IAppDbContext Context { get; }

        IRepository<Favourite> Favourites { get; }

        IRepository<Tweet> Tweets { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
