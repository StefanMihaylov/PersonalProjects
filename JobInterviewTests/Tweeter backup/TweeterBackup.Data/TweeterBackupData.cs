namespace TweeterBackup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using TweeterBackup.Data.Common.Repository;
    using TweeterBackup.Data.Model;

    public class TweeterBackupData : ITweeterBackupData
    {
        private readonly IDictionary<Type, object> repositories;

        private IAppDbContext context;

        public TweeterBackupData(IAppDbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IAppDbContext Context
        {
            get { return this.context; }
            private set { this.context = value; }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Favourite> Favourites
        {
            get { return this.GetRepository<Favourite>(); }
        }

        public IRepository<Tweet> Tweets
        {
            get { return this.GetRepository<Tweet>(); }
        }

        // common parts
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EfRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
