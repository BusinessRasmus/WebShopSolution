using FakeItEasy;
using Moq;
using WebShop.DataAccess;
using WebShop.DataAccess.Factory;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;
using WebShop.Shared.Notifications;

namespace WebShopTests.Tests
{
    public class UnitOfWorkTests
    {
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
