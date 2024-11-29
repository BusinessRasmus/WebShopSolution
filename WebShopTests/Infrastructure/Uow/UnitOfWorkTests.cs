using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Repositories.Interfaces;
using WebShop.Infrastructure.UnitOfWork;

namespace WebShopTests.Infrastructure.Uow
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
        public void GetRepository_WithValidParameters_ReturnsRepository()
        {
            // Act
            var result = _sutUow.Repository<Product>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Product>>(result);
        }

        [Fact]
        public void GetRepository_WithCustomerClass_ReturnsCustomerRepository()
        {
            // Act
            var result = _sutUow.Repository<Customer>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Customer>>(result);
        }

        [Fact]
        public void GetRepository_WithOrderClass_ReturnsOrderRepository()
        {
            // Act
            var result = _sutUow.Repository<Order>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Order>>(result);
        }
    }
}
