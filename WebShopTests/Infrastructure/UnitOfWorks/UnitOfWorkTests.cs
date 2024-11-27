using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Controllers;
using WebShop.DataAccess.DataAccess;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Factory;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;
using WebShop.Infrastructure.Notifications.Subjects;
using WebShop.Infrastructure.UnitOfWork;

namespace WebShopTests.Infrastructure.UnitOfWorks
{
    public class UnitOfWorkTests
    {
        // Fakes
        private readonly WebShopDbContext _context;
        private readonly IRepositoryFactory _factory;
        private readonly UnitOfWork _sutUow;


        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbUnitOfWork")
            .Options;

            _context = new WebShopDbContext(options);
            _factory = A.Fake<IRepositoryFactory>();
            _sutUow = new UnitOfWork(_context, _factory);
        }

        [Fact]
        public void GetProductRepository_WithValidParameters_ReturnsRepository()
        {
            // Act
            var result = _sutUow.Repository<Product>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Product>>(result);
        }

        [Fact]
        public void GetProductRepository_WithCustomerClass_ReturnsCustomerRepository()
        {
            // Act
            var result = _sutUow.Repository<Customer>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Customer>>(result);
        }
    }
}
