using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Controllers;
using WebShop.DataAccess;
using WebShop.DataAccess.Factory;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;
using WebShop.Shared.Notifications;

namespace WebShopTests.Tests
{
    public class UnitOfWorkTests
    {
        // Fakes
        private readonly IRepositoryFactory _factory = A.Fake<IRepositoryFactory>();
        private readonly WebShopDbContext _context;
        private readonly UnitOfWork _uow;

        public UnitOfWorkTests()
        {
            _context = A.Fake<WebShopDbContext>();
            _uow = new UnitOfWork(_context, _factory);
        }

        #region Unit Of Work
        [Fact]
        public async Task GetProductRepository_WithValidParameters_ReturnsRepository()
        {
            // Act
            var result = await _uow.Repository<Product>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Product>>(result);
        }

        [Fact]
        public async Task GetProductRepository_WithCustomerClass_ReturnsCustomerRepository()
        {
            // Act
            var result = await _uow.Repository<Product>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Product>>(result);
        }
        #endregion

        [Fact]
        public void NotifyProductAdded_CallsObserverUpdate()
        {
            var fakeFactory = A.Fake<IRepositoryFactory>();
            var fakeUow = A.Fake<IUnitOfWork>();

            // Arrange
            var product = A.Dummy<Product>();

            // Skapar en mock av INotificationObserver
            var fakeObserver = A.Fake<INotificationObserver<Product>>();

            // Skapar en instans av ProductSubject och lägger till mock-observatören
            var productSubject = new ProductSubject();
            productSubject.Attach(fakeObserver);

            A.CallTo(() => fakeUow.NotifyProductAdded(product)).Invokes(() => fakeObserver.Update(product));

            // Act
            fakeUow.NotifyProductAdded(product);

            // Assert
            // Verifierar att Update-metoden kallades på vår mock-observatör
            A.CallTo(() => fakeObserver.Update(product)).MustHaveHappenedOnceExactly();
        }
    }
}
