using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebShop.DataAccess.Repositories.Factory.RepositoryFactory;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.DataAccess;
using WebShop.Domain.Models;

namespace WebShop.DataAccess.Repositories.Factory
{
    public class RepositoryFactory(WebShopDbContext context) : IRepositoryFactory
    {
        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            //Nedanstående utkommenterat kan användas i de fall vi behöver olika repositories för olika entiteter.
            //if (typeof(TEntity) == typeof(Product))
            //    return (IRepository<TEntity>)new ProductRepository(context);

            return new Repository<TEntity>(context);
        }
    }

}
