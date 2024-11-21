using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Shared.Models;

namespace WebShopTests.RepositoryTestData
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
                new Product { Name = "Test", Amount = 10, Price = 29.80 },
                new Product { Name = "Test2", Amount = 5, Price = 19.80 },
                new Product { Name = "Test3", Amount = 15, Price = 9.80 },
                new Product { Name = "Test4", Amount = 20, Price = 39.80 },
                }

            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
