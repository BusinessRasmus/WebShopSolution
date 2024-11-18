using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Shared.Models;

namespace WebShop.DataAccess.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly WebShopDbContext _context;

        public OrderRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public Task Add(Order item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
