using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repositories
{
    public interface IRepository<TE> where TE : class
    {
        Task<TE> GetByIdAsync(int id);
        Task<IEnumerable<TE>> GetAllAsync();
        Task AddAsync(TE item);
        Task UpdateAsync(int id, TE item);
        Task Remove(int id);
    }
}
