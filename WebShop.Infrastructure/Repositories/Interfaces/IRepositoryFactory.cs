namespace WebShop.Infrastructure.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
    }

}
