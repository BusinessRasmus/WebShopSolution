﻿using WebShop.DataAccess.Repositories;
using WebShop.Domain.Models;

namespace WebShop.DataAccess.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();

        Task<IRepository<TEntity>> Repository<TEntity>() where TEntity : class;
    }
}

