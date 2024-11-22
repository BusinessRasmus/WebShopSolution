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
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IRepository<Product> _fakeRepository = A.Fake<IRepository<Product>>();
        private readonly ProductController _fakeController;

        // Not fakes
        private readonly IRepository<Product> _sutRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly RepositoryFactory _factory;
        private readonly WebShopDbContext _dbContext;

        public UnitOfWorkTests()
        {


            var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

            _dbContext = new WebShopDbContext(options);
            _factory = new RepositoryFactory(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext, _factory);
            _sutRepository = new Repository<Product>(_dbContext);
        }

        #region Unit Of Work

        [Fact]
        public async Task GetProductRepository_WithValidParameters_ReturnsRepository()
        {
            // Arrange
            A.CallTo(() => _fakeUow.Repository<Product>()).Returns(_fakeRepository);

            // Act
            var result = await _fakeUow.Repository<Product>();

            // Assert
            Assert.Equal(_fakeRepository, result);
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
            var fakeObserver = A.Fake<INotificationObserver>();

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
