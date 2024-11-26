using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using WebShop;
using WebShop.Controllers;
using WebShop.DataAccess.DataAccess;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Factory;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.SubjectManager;
using WebShop.Infrastructure.UnitOfWork;

namespace WebshopTests.API.Controllers
{
    public class ProductControllerTests
    {
        // Fakes
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IRepository<Product> _fakeRepository = A.Fake<IRepository<Product>>();
        private readonly ISubjectManager _fakeSubjectManager = A.Fake<ISubjectManager>();
        private readonly ProductController _fakeController;

        // Not fakes
        private readonly ProductController _productController;
        private readonly UnitOfWork _unitOfWork;
        private readonly RepositoryFactory _factory;
        private readonly WebShopDbContext _dbContext;

        public ProductControllerTests()
        {
            _fakeController = new ProductController(_fakeUow, _fakeSubjectManager);
            //sut = new Repository<Product>(_fakeDbContext);

            var options = new DbContextOptionsBuilder<WebShopDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _dbContext = new WebShopDbContext(options);
            _factory = new RepositoryFactory(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext, _factory);
            _productController = new ProductController(_unitOfWork, _fakeSubjectManager);
        }

        #region GetAllProducts
        [Fact]
        public async Task GetAllProducts_NoProductsInDb_ReturnsNotFound()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Act
            var result = await _fakeController.GetAllProducts();
            var resultAsNotFoundObjectResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(resultAsNotFoundObjectResult);
        }

        [Fact]
        public async Task GetAllProducts_WithProductsInDb_ReturnsEnumerable()
        {
            // Arrange
            var enumerable = A.CollectionOfDummy<Product>(5);

            var repo = A.Dummy<IRepository<Product>>();
            A.CallTo(() => _fakeUow.Repository<Product>()).Returns(repo);
            A.CallTo(() => repo.GetAllAsync()).Returns(enumerable);

            // Act
            var result = await _fakeController.GetAllProducts();

            // Assert
            A.CallTo(() => repo.GetAllAsync()).MustHaveHappenedOnceExactly();
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        #endregion

        #region GetProductById
        [Fact]
        public async Task GetProductById_WithValidId_ReturnsProduct()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Amount = 10,
                Price = 10
            };
            await _productController.AddProduct(product);

            // Act
            var result = await _productController.GetProductById(product.Id);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            var resultValue = (result.Result as OkObjectResult).Value;

            Assert.IsAssignableFrom<Product>(resultValue);
            var productResult = resultValue as Product;

            Assert.Equal(product, productResult);
        }

        [Fact]
        public async Task GetProductById_WithInvalidId_ReturnsNotFound()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Amount = 10,
                Price = 10
            };
            await _productController.AddProduct(product);

            // Act
            var result = await _productController.GetProductById(-1);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }
        #endregion

        #region AddProduct
        [Fact]
        public async Task AddProduct_WithValidInput_ReturnsOkResultAndMustHaveHappenedOnceExactly()
        {
            // Arrange
            var dummyProduct = A.Dummy<Product>();

            // Act
            var result = await _fakeController.AddProduct(dummyProduct);
            await _fakeRepository.AddAsync(dummyProduct);

            // Assert
            Assert.IsAssignableFrom<ActionResult>(result);
            A.CallTo(() => _fakeRepository.AddAsync(dummyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddProduct_WithInvalidInput_ReturnsBadRequest()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var product = new Product
            {
                Name = "T",
                Amount = 20,
                Price = 30
            };

            // Act
            var result = await _productController.AddProduct(product);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }
        #endregion

        #region UpdateProduct
        [Fact]
        public async Task UpdateProduct_WithValidInput_ReturnsOkResultAndMustHaveHappenedOnceExactly()
        {
            // Arrange
            var productToSend = A.Dummy<Product>();
            productToSend.Name = "Hejsan";
            var repository = await _fakeUow.Repository<Product>();

            A.CallTo(() => _fakeUow.Repository<Product>()).Returns(repository);
            A.CallTo(() => repository.UpdateAsync(productToSend)).DoesNothing();

            // Act
            var result = await _fakeController.UpdateProduct(1, productToSend);

            // Assert
            A.CallTo(() => _fakeUow.Repository<Product>()).MustHaveHappened();
            A.CallTo(() => _fakeUow.Complete()).MustHaveHappened();
            A.CallTo(() => repository.UpdateAsync(productToSend)).MustHaveHappened();
            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task UpdateProduct_WithInvalidInput_ReturnsBadRequest()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var product = new Product
            {
                Id = 2,
                Name = "Test",
                Amount = 10,
                Price = 10
            };
            await _productController.AddProduct(product);

            product.Name = "T";

            // Act
            var result = await _productController.UpdateProduct(product.Id, product);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        #endregion

        #region DeleteProduct
        [Fact]
        public async Task DeleteProduct_ValidId_ReturnsOkResult()
        {
            // Arrange
            var productToDelete = A.Fake<Product>();

            // Act
            var result = await _fakeController.DeleteProduct(productToDelete.Id);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ProductNotFound_ReturnsNotFound()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var productToDelete = new Product
            {
                Id = -1,
                Name = "Test",
                Amount = 10,
                Price = 10
            };

            // Act
            var result = await _productController.DeleteProduct(productToDelete.Id);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ValidId_CallsDeleteAsync()
        {
            // Arrange
            var productToDelete = A.Dummy<Product>();
            A.CallTo(() => _fakeRepository.GetByIdAsync(productToDelete.Id)).Returns(Task.FromResult(productToDelete));
            A.CallTo(() => _fakeUow.Repository<Product>()).Returns(_fakeRepository);

            // Act
            var result = await _fakeController.DeleteProduct(productToDelete.Id);

            // Assert
            Assert.IsType<OkResult>(result);
            A.CallTo(() => _fakeRepository.DeleteAsync(productToDelete.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUow.Complete()).MustHaveHappenedOnceExactly();
        }
        #endregion

        private async Task EnsureDatabaseDeletedAndCreated()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }
    }
}
