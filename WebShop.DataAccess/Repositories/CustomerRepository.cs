using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Shared.Models;

namespace WebShop.DataAccess.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly WebShopDbContext _context;

        public CustomerRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public async Task Add(Customer item)
        {
            await _context.AddAsync(item);
        }

        public Task<Customer> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
