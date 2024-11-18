using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repositories
{
    public class GenericRepository<TE> : IGenericRepository<TE> where TE : class
    {
        internal WebShopDbContext _context;
        internal DbSet<TE> _dbSet;

        public GenericRepository(WebShopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TE>();
        }

        public Task Add(TE item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TE>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TE> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(TE item)
        {
            throw new NotImplementedException();
        }
    }
}
