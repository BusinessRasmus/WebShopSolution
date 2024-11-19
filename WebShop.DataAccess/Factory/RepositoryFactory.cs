using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebShop.DataAccess.Factory.RepositoryFactory;
using WebShop.DataAccess.Repositories;
using WebShop.Shared.Models;

namespace WebShop.DataAccess.Factory
{
    public class RepositoryFactory(WebShopDbContext context) : IRepositoryFactory
    {
        public async Task<IRepository<TEntity>> CreateRepository<TEntity>() where TEntity : class
        {
            //TODO Ta bort nedanstående. Endast om repositories finns med annan funktionalitet?
            //if (typeof(TEntity) == typeof(Product))
            //    return (IRepository<TEntity>)new ProductRepository(context);

            return new Repository<TEntity>(context);
        }
    }

}
