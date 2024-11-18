using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repositories
{
    public interface IGenericRepository<TE> where TE : class
    {
        Task<TE> GetById(int id);
        Task<IEnumerable<TE>> GetAll();
        Task Add(TE item);
        Task Update(TE item);
        Task Remove(int id);
    }
}
