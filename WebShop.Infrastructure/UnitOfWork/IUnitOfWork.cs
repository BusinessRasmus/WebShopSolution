using WebShop.Infrastructure.Repositories.Interfaces;

namespace WebShop.Infrastructure.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}

