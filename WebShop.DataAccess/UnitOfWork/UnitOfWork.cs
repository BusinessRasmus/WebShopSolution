using WebShop.DataAccess.Repositories;
using WebShop.Shared.Notifications;
using WebShop.Shared.Models;
using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Factory;

namespace WebShop.DataAccess.UnitOfWork
{
    public class UnitOfWork(WebShopDbContext context, IRepositoryFactory factory, ProductSubject productSubject = null) : IUnitOfWork
    {
        private readonly ProductSubject _productSubject;

        // Om inget ProductSubject injiceras, skapa ett nytt
        // och Registrera standardobservatörer
        //TODO Avkommentera v
        //_productSubject = productSubject ?? new ProductSubject();
        //_productSubject.Attach(new EmailNotification());

        // Konstruktor används för tillfället av Observer pattern

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }

        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await context.DisposeAsync();
        }

        public async Task<IRepository<TEntity>> Repository<TEntity>() where TEntity : class
        {
            var repository = await factory.CreateRepository<TEntity>();
            return repository;
        }
    }
}
