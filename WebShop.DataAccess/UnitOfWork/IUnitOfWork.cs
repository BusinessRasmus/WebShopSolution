using WebShop.DataAccess.Repositories;
using WebShop.Shared.Models;

namespace WebShop.DataAccess.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable //TODO Ska ärva av IDisposible?
    {
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<Customer> CustomerRepository { get; }

        Task Complete();

         // Repository för produkter
         // Sparar förändringar (om du använder en databas)
        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

