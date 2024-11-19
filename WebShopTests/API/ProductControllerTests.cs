using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop;
using WebShop.Controllers;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;

public class ProductControllerTests
{
    private readonly IUnitOfWork fakeUow = A.Fake<IUnitOfWork>();
    private readonly IRepository<Product> fakeRepository = A.Fake<IRepository<Product>>();
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _controller = new ProductController(fakeUow);
    }

    #region Mocks

    [Fact]
    public async Task GetProductRepository_WithValidParameters_ReturnsRepository()
    {
        // Arrange
        A.CallTo(() => fakeUow.Repository<Product>()).Returns(fakeRepository);

        // Act
        var result = await fakeUow.Repository<Product>();

        // Assert
        Assert.Equal(fakeRepository, result);
    }

    [Fact]
    public async Task GetProductById_WithValidInput_ReturnsProduct()
    {
        // Arrange
        var fakeProductRepo = await fakeUow.Repository<Product>();
        var dummyProduct = A.Dummy<Product>();
        A.CallTo(() => fakeProductRepo.GetByIdAsync(2)).Returns(dummyProduct);

        // Act
        var result = await fakeProductRepo.GetByIdAsync(2);

        // Assert
        Assert.Equal(dummyProduct, result);
        A.CallTo(() => fakeProductRepo.GetByIdAsync(2)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddProduct_WithValidInput_ReturnsOkResultAndMustHaveHappenedOnceExactly()
    {
        // Arrange
        var dummyProduct = A.Dummy<Product>();

        // Act
        var result = await _controller.AddProduct(dummyProduct);
        await fakeRepository.AddAsync(dummyProduct);

        // Assert
        Assert.IsAssignableFrom<ActionResult>(result);
        A.CallTo(() => fakeRepository.AddAsync(dummyProduct)).MustHaveHappenedOnceExactly();
    }

    #endregion

    #region Unit Tests
    [Fact]
    public async Task DeleteProduct_ValidId_ReturnsOk()
    {
        // Arrange
        var productToDelete = A.Fake<Product>();


        // Act
        await _controller.DeleteProduct(productToDelete.Id);

        // Assert
        A.CallTo(() => fakeUow.Repository<Product>().Result.DeleteAsync(productToDelete.Id)).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteProduct_InvalidModelState_ReturnsBadRequest()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task DeleteProduct_ProductNotFound_ReturnsNotFound()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task DeleteProduct_DeleteFails_ThrowsAggregateException()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task DeleteProduct_ValidId_CallsDeleteAsync()
    {
        // Arrange

        // Act

        // Assert
    }





    #endregion
}
