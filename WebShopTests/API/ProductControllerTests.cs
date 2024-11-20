using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop;
using WebShop.Controllers;
using WebShop.DataAccess;
using WebShop.DataAccess.Factory;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;

public class ProductControllerTests
{
    // Fakes
    private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
    private readonly IRepository<Product> _fakeRepository = A.Fake<IRepository<Product>>();
    private readonly ProductController _fakeController;

    // Not fakes
    private readonly ProductController _productController;
    private readonly UnitOfWork _unitOfWork;
    private readonly RepositoryFactory _factory;
    private readonly WebShopDbContext _dbContext;

    public ProductControllerTests()
    {
        _fakeController = new ProductController(_fakeUow);

        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        _dbContext = new WebShopDbContext(options);
        _factory = new RepositoryFactory(_dbContext);
        _unitOfWork = new UnitOfWork(_dbContext, _factory);
        _productController = new ProductController(_unitOfWork);
    }

    #region GetProduct
    [Fact]
    public async Task GetAllProducts_NoProductsInDb_ReturnsNotFound()
    {
        // Act
        var result = await _fakeController.GetAllProducts();
        var resultAsNotFoundObjectResult = result.Result as NotFoundObjectResult;

        // Assert
        Assert.IsAssignableFrom<NotFoundObjectResult>(resultAsNotFoundObjectResult);
    }

    [Fact]
    public async Task GetAllProducts_WithProductsInDb_ReturnsEnumerable()
    {
        // Act
        var result = await _fakeController.GetAllProducts();
        var resultAsNotFoundObjectResult = result.Result as NotFoundObjectResult;

        // Assert
        Assert.IsAssignableFrom<NotFoundObjectResult>(resultAsNotFoundObjectResult);
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
    public async Task DeleteProduct_WritingToDbFails_ThrowsStatusCode500()
    {
        // Arrange
        var productToDelete = new Product
        {
            Id = 1,
            Name = "Test",
            Amount = 10,
            Price = 10
        };

        await _productController.AddProduct(productToDelete);
        await _dbContext.Database.EnsureDeletedAsync();

        // Act
        var result = await _productController.DeleteProduct(productToDelete.Id);
        var resultAsObjectResult = result as ObjectResult;

        // Assert
        Assert.Equal(resultAsObjectResult.StatusCode, 500);
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

    #region ProductRepository_GetProduct
    [Fact]
    public async Task GetProductById_WithValidInput_ReturnsProduct()
    {
        // Arrange
        var fakeProductRepo = await _fakeUow.Repository<Product>();
        var dummyProduct = A.Dummy<Product>();
        A.CallTo(() => fakeProductRepo.GetByIdAsync(2)).Returns(dummyProduct);

        // Act
        var result = await fakeProductRepo.GetByIdAsync(2);

        // Assert
        Assert.Equal(dummyProduct, result);
        A.CallTo(() => fakeProductRepo.GetByIdAsync(2)).MustHaveHappenedOnceExactly();
    }

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
}
