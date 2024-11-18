using WebShop.DataAccess.Repositories;
using WebShop.Shared.Notifications;
using WebShop.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace WebShop.DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public WebShopDbContext _context;

        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<Customer> CustomerRepository { get; }


        // Hämta produkter från repository

        private readonly ProductSubject _productSubject;

        // Konstruktor används för tillfället av Observer pattern
        public UnitOfWork(WebShopDbContext context, ProductSubject productSubject = null)
        {
            _context = context;
            ProductRepository = new GenericRepository<Product>(context);
            OrderRepository = new GenericRepository<Order>(context);
            CustomerRepository = new GenericRepository<Customer>(context);


            // Om inget ProductSubject injiceras, skapa ett nytt
            _productSubject = productSubject ?? new ProductSubject();

            // Registrera standardobservatörer
            _productSubject.Attach(new EmailNotification());
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
