using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repositories;

namespace WebShop.DataAccess.Repositories.Factory
{
    public interface IRepositoryFactory
    {
        Task<IRepository<TEntity>> CreateRepository<TEntity>() where TEntity : class;
    }

}
