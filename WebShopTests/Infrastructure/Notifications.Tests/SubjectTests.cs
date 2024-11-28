using FakeItEasy;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShopTests.Infrastructure.Notifications.Tests
{
    public class SubjectTests
    {
        // Test av Subject-klass av typen Product.
        private readonly Subject<Product> _subject;

        public SubjectTests()
        {
            _subject = new Subject<Product>();
        }

        [Fact]
        public void Notify_MustCallObserverUpdateOnceExactly()
        {
            // Arrange
            var product = A.Dummy<Product>();
            var fakeObserver = A.Fake<INotificationObserver<Product>>();
            _subject.Attach(fakeObserver);

            // Act
            _subject.Notify(product);

            // Assert
            A.CallTo(() => fakeObserver.Update(A<Product>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Attach_MustCallEachObserverUpdateOnceExactly()
        {
            // Arrange
            var product = A.Dummy<Product>();
            var fakeObserverOne = A.Fake<INotificationObserver<Product>>();
            var fakeObserverTwo = A.Fake<INotificationObserver<Product>>();
            _subject.Attach(fakeObserverOne);
            _subject.Attach(fakeObserverTwo);

            // Act
            _subject.Notify(product);

            // Assert
            A.CallTo(() => fakeObserverOne.Update(A<Product>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeObserverTwo.Update(A<Product>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Detach_MustCallOneObserverUpdateOnceExactly()
        {
            // Arrange
            var product = A.Dummy<Product>();
            var fakeObserverOne = A.Fake<INotificationObserver<Product>>();
            var fakeObserverTwo = A.Fake<INotificationObserver<Product>>();
            _subject.Attach(fakeObserverOne);
            _subject.Attach(fakeObserverTwo);

            //Remove one observer
            _subject.Detach(fakeObserverTwo);

            // Act
            _subject.Notify(product);

            // Assert
            A.CallTo(() => fakeObserverOne.Update(A<Product>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeObserverTwo.Update(A<Product>._)).MustNotHaveHappened();
        }
    }
}
