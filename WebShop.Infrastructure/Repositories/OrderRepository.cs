using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Repositories.Interfaces;

namespace WebShop.Infrastructure.Repositories
{
    internal class OrderRepository : Repository<Order>
    {

        internal WebShopDbContext _context;
        internal DbSet<Order> _orders;
        internal DbSet<OrderDetail> _orderDetails;

        public OrderRepository(WebShopDbContext context)
        {
            _context = context;
            _orders = context.Set<Order>();
            _orderDetails = context.Set<OrderDetail>();
        }

        public Task AddAsync(Order item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
