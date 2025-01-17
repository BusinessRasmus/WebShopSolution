﻿using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;

namespace WebShop.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        internal DbSet<Order> _orderDbSet;
        internal DbSet<OrderDetail> _orderDetailDbSet;

        public OrderRepository(WebShopDbContext context) : base(context)
        {
            _orderDbSet = context.Orders;
            _orderDetailDbSet = context.OrderDetails;
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderDbSet
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Customer)
                .ToListAsync();
        }

        public override async Task<Order> GetByIdAsync(int id)
        {
            var order = await _orderDbSet
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }
    }
}
