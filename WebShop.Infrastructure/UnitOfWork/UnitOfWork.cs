using WebShop.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.DataAccess;
using WebShop.Infrastructure.Notifications.Subjects;
using WebShop.Domain.Models;
using WebShop.DataAccess.Repositories.Factory;

namespace WebShop.Infrastructure.UnitOfWork
{
    public class UnitOfWork(WebShopDbContext context, IRepositoryFactory factory) : IUnitOfWork
    {
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await context.DisposeAsync();
        }

        public  IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var repository = factory.CreateRepository<TEntity>();
            return repository;
        }
    }
}
