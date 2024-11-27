using System.Collections;
using WebShop.Domain.Models;

namespace WebShopTests.TestData
{
    internal class RepositoryTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield 
                return new object[] 
            {
                new Product[]
                {
                new Product { Name = "Test", Amount = 10, Price = 25 },
                new Product { Name = "Test2", Amount = 5, Price = 19.80 },
                new Product { Name = "Test3", Amount = 15, Price = 9.80 },
                new Product { Name = "Test4", Amount = 20, Price = 39.80 }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
