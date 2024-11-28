using WebShop.Infrastructure.DataAccess;

namespace WebShop.Infrastructure.Repositories.Factory
{
    public class RepositoryFactory(WebShopDbContext context) : IRepositoryFactory
    {
        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            //Nedanstående utkommenterat kan användas i de fall vi behöver olika repositories för olika entiteter (i optimiseringssyfte).
            //if (typeof(TEntity) == typeof(Product))
            //    return (IRepository<TEntity>)new ProductRepository(context);

            return new Repository<TEntity>(context);
        }
    }

}
