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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var repository = await _unitOfWork.Repository<Product>();
            var products = await repository.GetAllAsync();

            if (!products.Any())
                return NotFound(Enumerable.Empty<Product>());

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById([FromRoute] int id)
        {
            var repository = await _unitOfWork.Repository<Product>();
            var product = await repository.GetByIdAsync(id);

            if (product is null)
                return NotFound(Enumerable.Empty<Product>());

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (product is null)
                return BadRequest();

            var repository = await _unitOfWork.Repository<Product>();
            
            await repository.AddAsync(product);
            await _unitOfWork.Complete();

            //TODO Lägg in notification här.
            // Notifierar observatörer om att en ny produkt har lagts till

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (product is null)
                return BadRequest();

            var repository = await _unitOfWork.Repository<Product>();

            await repository.UpdateAsync(id, product);
            await _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productToDelete = await _unitOfWork.Repository<Product>().Result.GetByIdAsync(id);

            if (productToDelete is null)
                return NotFound($"No product with id {id} was found.");

            try
            {
                await _unitOfWork.Repository<Product>().Result.DeleteAsync(productToDelete.Id);
            }
            catch
            {
                throw new AggregateException("Something went wrong when deleting the specified product.");
            }

            await _unitOfWork.Complete();

            return Ok();
        }
    }
}
