﻿namespace ChatServer.Common.Interfaces
{
    using System.Data.Entity;

    public interface IUnitOfWork<C> where C : IDbContext
    {
        Database Database { get; }

        // C Context { get; }

        int SaveChanges();
    }
}
