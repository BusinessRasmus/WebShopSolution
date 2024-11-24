using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Controllers;
using WebShop.DataAccess;
using WebShop.DataAccess.Factory;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;
using WebShopTests.TestData;

namespace WebShopTests.DataAccess.Repositories
{
    public class RepositoryTests
    {
        // Not fakes
        private readonly IRepository<Product> _sutRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly RepositoryFactory _factory;
        private readonly WebShopDbContext _dbContext;

        //TODO Ta bort skiten eller refaktorera
        //private readonly DbContextOptions<WebShopDbContext> fakeOptions = A.Fake<DbContextOptions<WebShopDbContext>>();
        //private readonly WebShopDbContext _fakeDbContext;
        //private readonly IRepository<Product> _fakeRepository;


        public RepositoryTests()
        {
            //_fakeDbContext = new WebShopDbContext(fakeOptions);
            //_fakeRepository = new Repository<Product>(_fakeDbContext);

            var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

            _dbContext = new WebShopDbContext(options);
            _factory = new RepositoryFactory(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext, _factory);
            _sutRepository = new Repository<Product>(_dbContext);
        }

        #region Repository_GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_WithValidInput_ReturnsProduct()
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

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sutRepository.GetByIdAsync(1);

            // Assert
            Assert.Equal(product.Name, result.Name);
            await _dbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidInput_ReturnsNull()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var repo = await _unitOfWork.Repository<Product>();

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Amount = 10,
                Price = 10
            };
            await _dbContext.AddAsync(product);

            // Act
            var result = await repo.GetByIdAsync(2);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Repository_GetAllAsync
        [Fact]
        public async Task GetAllAsync_WithValidInput_ReturnsListOfProducts()
        {
            await EnsureDatabaseDeletedAndCreated();
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Test",
                Amount = 10,
                Price = 10
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Test2",
                Amount = 20,
                Price = 20
            };

            await _dbContext.AddAsync(product1);
            await _dbContext.AddAsync(product2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sutRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenDbIsEmpty_ReturnsNull()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var repo = await _unitOfWork.Repository<Product>();

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            Assert.Empty(result);
            await _dbContext.Database.EnsureDeletedAsync();
        }
        #endregion

        #region Repository_AddAsync
        //[Theory]
        //[ClassData(typeof(RepositoryTestData))]
        //public async Task AddAsync_GetAllAsync_WithValidInput_ReturnsListOfProducts(Product[] input)
        //{
        //    await EnsureDatabaseDeletedAndCreated();

        //    var repo = await _unitOfWork.Repository<Product>();
        //    // Arrange
        //    foreach (var p in input)
        //    {
        //        // Act
        //        await repo.AddAsync(p);
        //        await _dbContext.SaveChangesAsync();

        //        var result = await repo.GetByIdAsync(p.Id);

        //        // Assert
        //        Assert.Equal(p.Name, result.Name);
        //    }

        //    // Additional assert
        //    var productsInDb = await repo.GetAllAsync();
        //    Assert.Equal(input.Count(), productsInDb.Count());
            
        //}
        #endregion

        #region Repository_UpdateAsync
        [Fact]
        public async Task UpdateAsync_WithValidInput_ReturnsProduct()
        {
            await EnsureDatabaseDeletedAndCreated();

            // Arrange
            var repo = await _unitOfWork.Repository<Product>();

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Amount = 10,
                Price = 10
            };

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            product.Name = "Test2";

            // Act
            await repo.UpdateAsync(product);
            await _dbContext.SaveChangesAsync();

            var result = await repo.GetByIdAsync(1);

            // Assert
            Assert.Equal(product.Name, result.Name);
        }
        #endregion

        #region Repository_DeleteAsync
        [Fact]
        public async Task DeleteAsync_WithValidInput_ReturnsNull()
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

            await _sutRepository.AddAsync(product);
            await _unitOfWork.Complete();

            // Act
            await _sutRepository.DeleteAsync(1);
            await _dbContext.SaveChangesAsync();

            var result = await _sutRepository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ReturnsProduct()
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

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            // Act
            await _sutRepository.DeleteAsync(2);
            await _dbContext.SaveChangesAsync();

            var result = await _sutRepository.GetByIdAsync(1);

            // Assert
            Assert.Equal(product.Name, result.Name);
        }
        #endregion

        private async Task EnsureDatabaseDeletedAndCreated()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }
    }
}
