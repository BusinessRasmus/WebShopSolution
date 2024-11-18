using Microsoft.AspNetCore.Mvc;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            // Behöver använda repository via Unit of Work för att hämta produkter
            return Ok();
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.Complete();
            // Lägger till produkten via repository

            // Sparar förändringar

            // Notifierar observatörer om att en ny produkt har lagts till

            return Ok();
        }
    }
}
