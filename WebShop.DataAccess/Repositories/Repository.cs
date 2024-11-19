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

        public async Task AddAsync(TE item)
        {
            await _dbSet.AddAsync(item);
        }

        public async Task<IEnumerable<TE>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
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

        public async Task Remove(int id)
        {
            var itemToRemove = await _dbSet.FindAsync(id);
            if (itemToRemove is null)
                return;

            _dbSet.Remove(itemToRemove);
        }

        public async Task UpdateAsync(int id, TE item)
        {
            var result = await _dbSet.FindAsync(id);
            if (result is null)
                return;

            await _dbSet.Update(item).ReloadAsync();
        }
    }
}
