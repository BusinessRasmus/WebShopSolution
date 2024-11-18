using WebShop.Models;

namespace WebShop.Repositories
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(); // Hämtar alla produkter
        void Add(Product product); // Lägger till en ny produkt

        //TODO: Add remaining CRUD if needed.
    }
}
