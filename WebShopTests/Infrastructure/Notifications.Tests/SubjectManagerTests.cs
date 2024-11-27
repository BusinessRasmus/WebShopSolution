using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repositories;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Factory;
using WebShop.Infrastructure.Notifications.SubjectManager;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShopTests.Infrastructure.Notifications.Tests
{
    public class SubjectManagerTests
    {
        private readonly ISubjectFactory _factory = A.Fake<ISubjectFactory>();
        private readonly SubjectManager _subjectManager;

        public SubjectManagerTests()
        {
            _subjectManager = new SubjectManager(_factory);
        }

        [Fact]
        public void Subject_WithValidType_ReturnsRepository()
        {
            var subject = A.Fake<ISubject<Product>>();

            // Arrange
            A.CallTo(() => _factory.CreateSubject<Product>()).Returns(subject);

            // Act
            var result = _subjectManager.Subject<Product>();

            // Assert
            A.CallTo(() => _factory.CreateSubject<Product>()).MustHaveHappenedOnceExactly();
            Assert.Equal(subject, result);
        }
    }
}
