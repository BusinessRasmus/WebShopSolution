//using FakeItEasy;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebShop.Controllers;
//using WebShop.DataAccess.Factory;
//using WebShop.DataAccess.Repositories;
//using WebShop.DataAccess.UnitOfWork;
//using WebShop.DataAccess;
//using WebShop.Shared.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace WebShopTests.API.Controllers
//{
//    public class CustomerControllerTests
//    {
//        // Fakes
//        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
//        private readonly IRepository<Customer> _fakeRepository = A.Fake<IRepository<Customer>>();
//        private readonly CustomerController _fakeSut;

//        // Not fakes
//        private readonly CustomerController _sut;
//        private readonly UnitOfWork _unitOfWork;
//        private readonly RepositoryFactory _factory;
//        private readonly WebShopDbContext _dbContext;

//        public CustomerControllerTests()
//        {
//            _fakeSut = new CustomerController(_fakeUow);

//            var options = new DbContextOptionsBuilder<WebShopDbContext>()
//                .UseInMemoryDatabase("TestDb")
//                .Options;

//            _dbContext = new WebShopDbContext(options);
//            _factory = new RepositoryFactory(_dbContext);
//            _unitOfWork = new UnitOfWork(_dbContext, _factory);
//            _sut = new CustomerController(_unitOfWork);
//        }

//        #region GetAllCustomers
//        [Fact]
//        public async Task GetAllCustomers_WithEnumerableReturn_ReturnsEnumerableOfCustomers()
//        {
//            // Arrange
//            var repo = await _fakeUow.Repository<Customer>();
//            A.CallTo(() => _fakeUow.Repository<Customer>()).Returns(repo);
//            A.CallTo(() => repo.GetAllAsync()).Returns(A.CollectionOfDummy<Customer>(2));

//            // Act
//            var result = await _fakeSut.GetAllCustomers();
//            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
//            var resultAsOkObjectResult = result.Result as OkObjectResult;

//            Assert.IsAssignableFrom<IEnumerable<Customer>>(resultAsOkObjectResult.Value);
//            var resultAsIenumerable = resultAsOkObjectResult.Value as IEnumerable<Customer>;
//            var count = resultAsIenumerable.Count();

//            // Assert
//            Assert.Equal(2, count);
//        }

//        [Fact]
//        public async Task GetAllCustomers_WithEmptyReturn_ReturnsNotFound()
//        {
//            // Arrange
//            var getResponse = Enumerable.Empty<Customer>();
//            var repo = await _fakeUow.Repository<Customer>();
//            A.CallTo(() => repo.GetAllAsync()).Returns(getResponse);

//            // Act
//            var result = await _fakeSut.GetAllCustomers();

//            // Assert
//            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
//        }
//        #endregion

//        #region GetCustomerById
//        [Fact]
//        public async Task GetCustomerById_WithValidId_ReturnsCustomer() 
//        {
//            // Arrange
//            var customer = A.Dummy<Task<Customer>>();

//            var repo = await _fakeUow.Repository<Customer>();
//            A.CallTo(() => _fakeUow.Repository<Customer>()).Returns(repo);
//            A.CallTo(() => repo.GetByIdAsync(0)).Returns(customer);

//            // Act
//            var result = await _fakeSut.GetCustomerById(0);

//            // Assert
//            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task GetCustomerById_WithInvalidId_ReturnsNotFound()
//        {
//            // Arrange
//            await EnsureDatabaseDeletedAndCreated();

//            // Act
//            var result = await _sut.GetCustomerById(1);

//            // Assert
//            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
//        }
//        #endregion

//        #region AddCustomer

//        [Fact]
//        public async Task AddCustomer_WithValidInput_ReturnsOk()
//        {
//            // Arrange
//            await EnsureDatabaseDeletedAndCreated();
//            Customer customerToAdd = new Customer
//            {
//                FirstName = "Test",
//                Email = "test@test.com",
//            };

//            // Act
//            var result = await _sut.AddCustomer(customerToAdd);

//            // Assert
//            Assert.IsAssignableFrom<OkResult>(result);
//        }

//        [Fact]
//        public async Task AddCustomer_WithInvalidInput_ReturnsBadStatus()
//        {
//            // Arrange
//            await EnsureDatabaseDeletedAndCreated();
//            Customer customerToAdd = new Customer
//            {
//                FirstName = "Tes<Script>t",
//                Email = "test@test.com",
//            };

//            // Act
//            var result = await _sut.AddCustomer(customerToAdd);

//            // Assert
//            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
//        }
//        #endregion



//        private async Task EnsureDatabaseDeletedAndCreated()
//        {
//            await _dbContext.Database.EnsureDeletedAsync();
//            await _dbContext.Database.EnsureCreatedAsync();
//        }
//    }
//}
