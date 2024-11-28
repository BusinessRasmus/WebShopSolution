namespace WebShop.Infrastructure.Repositories.Factory
{
    public interface IRepositoryFactory
    {
        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
    }

}
