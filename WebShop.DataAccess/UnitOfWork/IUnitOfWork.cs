using WebShop.DataAccess.Factory;
using WebShop.DataAccess.Repositories;
using WebShop.Shared.Models;

namespace WebShop.DataAccess.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();

        Task<IRepository<TEntity>> Repository<TEntity>() where TEntity : class;

        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

