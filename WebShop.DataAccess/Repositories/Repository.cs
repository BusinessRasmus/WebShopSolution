using Microsoft.EntityFrameworkCore;

namespace WebShop.DataAccess.Repositories
{
    public class Repository<TE> : IRepository<TE> where TE : class
    {
        internal WebShopDbContext _context;
        internal DbSet<TE> _dbSet;

        public Repository(WebShopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TE>();
        }

        public async Task<IEnumerable<TE>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }

        public async Task<TE> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result is null)
            {
                return await Task.FromResult<TE>(null);
            }
            return result;
        }

        public async Task AddAsync(TE item)
        {
            await _dbSet.AddAsync(item);
        }

        public async Task UpdateAsync(TE entity) => _dbSet.Update(entity);

        public async Task DeleteAsync(int id)
        {
            var itemToRemove = await _dbSet.FindAsync(id);
            if (itemToRemove is null)
                return;

            _dbSet.Remove(itemToRemove);
        }
    }
}
