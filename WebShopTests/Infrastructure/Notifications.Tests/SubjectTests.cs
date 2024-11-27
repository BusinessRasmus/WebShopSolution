using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repositories.Factory;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;
using WebShop.Infrastructure.Notifications.Subjects;
using WebShop.Infrastructure.UnitOfWork;

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
            var fakeObserver = A.Fake<INotificationObserver<Product>>();
            _subject.Attach(fakeObserver);

            // Act
            _subject.Notify(new Product());

            // Assert
            A.CallTo(() => fakeObserver.Update(A<Product>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Attach_MustCallEachObserverUpdateOnceExactly()
        {
            // Arrange
            var fakeObserverOne = A.Fake<INotificationObserver<Product>>();
            var fakeObserverTwo = A.Fake<INotificationObserver<Product>>();
            _subject.Attach(fakeObserverOne);
            _subject.Attach(fakeObserverTwo);

            // Act
            _subject.Notify(new Product());

            // Assert
            A.CallTo(() => fakeObserverOne.Update(A<Product>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeObserverTwo.Update(A<Product>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Detach_MustCallOneObserverUpdateOnceExactly()
        {
            // Arrange
            var fakeObserverOne = A.Fake<INotificationObserver<Product>>();
            var fakeObserverTwo = A.Fake<INotificationObserver<Product>>();
            _subject.Attach(fakeObserverOne);
            _subject.Attach(fakeObserverTwo);

            //Remove one observer
            _subject.Detach(fakeObserverTwo);

            // Act
            _subject.Notify(new Product());

            // Assert
            A.CallTo(() => fakeObserverOne.Update(A<Product>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeObserverTwo.Update(A<Product>._)).MustNotHaveHappened();
        }
    }
}
