using Microsoft.EntityFrameworkCore;
using WebShop.Controllers;
using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Notifications.Factory;
using WebShop.Infrastructure.Notifications.SubjectManager;
using WebShop.Infrastructure.Repositories.Factory;
using WebShop.Infrastructure.UnitOfWork;

namespace WebShopTests.Controllers
{
    /*
     * Skapad i debugging-syfte
     */
    public class OrderCreationTest
    {
        private readonly WebShopDbContext _dbContext;
        private readonly IRepositoryFactory _repoFactory;
        private readonly ISubjectFactory _subjectFactory;
        private readonly ISubjectManager _subjectManager;
        private readonly IUnitOfWork _unitOfWork;

        private readonly OrderController _orderController;
        private readonly ProductController _productController;
        private readonly CustomerController _customerController;

        public OrderCreationTest()
        {
            var options = new DbContextOptionsBuilder<WebShopDbContext>()
                .UseInMemoryDatabase("TestDbOrderCreation")
                .Options;

            _dbContext = new WebShopDbContext(options);
            _repoFactory = new RepositoryFactory(_dbContext);
            _subjectFactory = new SubjectFactory();
            _subjectManager = new SubjectManager(_subjectFactory);
            _unitOfWork = new UnitOfWork(_dbContext, _repoFactory);

            _orderController = new OrderController(_unitOfWork);
            _productController = new ProductController(_unitOfWork, _subjectManager);
            _customerController = new CustomerController(_unitOfWork);
        }

        [Fact]
        public async Task CreateProductAndCustomerAndOrder()
        {
            // Arrange
            await EnsureDatabaseDeletedAndCreated();

            var product = new Product
            {
                Name = "TestProduct",
                Price = 100,
                Stock = 50
            };

            var customer = new Customer
            {
                FirstName = "Test",
                Email = "Hey"
            };

            var newOrder = new Order
            {
                CustomerFirstName = customer.FirstName,
                OrderStatus = "Pending",
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductId = product.Id,
                        Quantity = 5
                    }
                }
            };

            // Act
            await _productController.AddProduct(product);
            await _customerController.AddCustomer(customer);
            await _orderController.AddOrder(newOrder);

            // Assert
            var result = _dbContext.Orders;
            await result.ToListAsync();

            Assert.Single(result);
        }

        private async Task EnsureDatabaseDeletedAndCreated()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }

    }
}
