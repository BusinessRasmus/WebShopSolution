using WebShop.Shared.Models;

namespace WebShop.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopDbContext _context;

        public ProductRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product item)
        {
                await _context.Products.AddAsync(item);
        }

        public async Task<Product> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
