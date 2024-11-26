using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Controllers;
using WebShop.DataAccess.DataAccess;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Factory;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShopTests.DataAccess
{
    public class UnitOfWorkTests
    {
        // Fakes

        private readonly WebShopDbContext _context;
        private readonly IRepositoryFactory _factory;
        private readonly UnitOfWork _fakeUow;


        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new WebShopDbContext(options);
            _factory = A.Fake<IRepositoryFactory>();
            _fakeUow = new UnitOfWork(_context, _factory);
        }

        #region Unit Of Work
        [Fact]
        public async Task GetProductRepository_WithValidParameters_ReturnsRepository()
        {
            // Act
            var result = await _fakeUow.Repository<Product>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Product>>(result);
        }

        [Fact]
        public async Task GetProductRepository_WithCustomerClass_ReturnsCustomerRepository()
        {
            // Act
            var result = await _fakeUow.Repository<Customer>();

            // Assert
            Assert.IsAssignableFrom<IRepository<Customer>>(result);
        }
        #endregion

        //[Fact]
        //public void NotifyProductAdded_CallsObserverUpdate()
        //{
        //    var fakeFactory = A.Fake<IRepositoryFactory>();
        //    var fakeUow = A.Fake<IUnitOfWork>();

        //    // Arrange
        //    var product = A.Dummy<Product>();

        //    // Skapar en mock av INotificationObserver
        //    var fakeObserver = A.Fake<INotificationObserver<Product>>();

        //    // Skapar en instans av ProductSubject och lägger till mock-observatören
        //    var productSubject = new ProductSubject();
        //    productSubject.Attach(fakeObserver);

        //    A.CallTo(() => fakeUow.NotifyProductAdded(product)).Invokes(() => fakeObserver.Update(product));

        //    // Act
        //    fakeUow.NotifyProductAdded(product);

        //    // Assert
        //    // Verifierar att Update-metoden kallades på vår mock-observatör
        //    A.CallTo(() => fakeObserver.Update(product)).MustHaveHappenedOnceExactly();
        //}
    }
}
