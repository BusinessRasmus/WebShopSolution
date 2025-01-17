﻿using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Repositories;
using WebShop.Infrastructure.Repositories.Interfaces;

namespace WebShopTests.Infrastructure.Repositories.Tests
{
    public class OrderRepositoryTests
    {
        private readonly IRepository<Order> _sutRepository;
        private readonly WebShopDbContext _dbContext;

        public OrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase("TestDbOrderRepository")
            .Options;

            _dbContext = new WebShopDbContext(options);
            _sutRepository = new Repository<Order>(_dbContext);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEnumerableOfOrder()
        {
            // Arrange
            await EnsureDatabaseDeletedAndCreated();
            var order = new Order
            {
                Id = 1,
                CustomerFirstName = "Test",
                OrderStatus = "Pending",
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderId = 1,
                        Quantity = 4,
                        Product = new Product
                            {
                                Name = "Test",
                                Price = 10,
                                Stock = 10
                            },
                    }
                },
                Customer = new Customer
                {
                    FirstName = "Test",
                    Email = "joe@mail.com"
                }
            };
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sutRepository.GetAllAsync();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsOrder()
        {
            // Arrange
            await EnsureDatabaseDeletedAndCreated();

            var order = new Order
            {
                Id = 1,
                CustomerFirstName = "Test",
                OrderStatus = "Pending",
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderId = 1,
                        Quantity = 4,
                        Product = new Product
                            {
                                Name = "Test",
                                Price = 10,
                                Stock = 10
                            },
                    }
                },
                Customer = new Customer
                {
                    FirstName = "Test",
                    Email = "joe@mail.com"
                }
            };
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sutRepository.GetByIdAsync(1);

            // Assert
            Assert.Equal(order, result);
        }

        private async Task EnsureDatabaseDeletedAndCreated()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }
    }
}
