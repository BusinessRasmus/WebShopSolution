﻿using Microsoft.EntityFrameworkCore;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Repositories.Interfaces;

namespace WebShop.Infrastructure.Repositories
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

        public virtual async Task<IEnumerable<TE>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }

        public virtual async Task<TE> GetByIdAsync(int id)
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

        public void Update(TE entity) => _dbSet.Update(entity);

        public async Task DeleteAsync(int id)
        {
            var itemToRemove = await _dbSet.FindAsync(id);
            if (itemToRemove is null)
                return;

            _dbSet.Remove(itemToRemove);
        }
    }
}
