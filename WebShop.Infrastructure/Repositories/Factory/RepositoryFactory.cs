using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Repositories.Interfaces;

namespace WebShop.Infrastructure.Repositories.Factory
{
    public class RepositoryFactory(WebShopDbContext context) : IRepositoryFactory
    {
        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            //Nedanstående utkommenterat kan utökas i de fall vi behöver repositories för ytterligare entiteter (i optimiseringssyfte).
            if (typeof(TEntity) == typeof(Order))
                return (IRepository<TEntity>)new OrderRepository(context);

            return new Repository<TEntity>(context);
        }
    }

}
