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

        // Endpoint f�r att h�mta alla produkter
        //[HttpGet]
        //public Task <ActionResult<IEnumerable<Product>>> GetProducts()
        //{
            
        //    //return Ok();
        //}

        // Endpoint f�r att l�gga till en ny produkt
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            if (product is null)
                return BadRequest();

            var repository = await _unitOfWork.Repository<Product>();
            // L�gger till produkten via repository
            await repository.AddAsync(product);

            // Sparar f�r�ndringar
            await _unitOfWork.Complete();

            //TODO L�gg in notification h�r.
            // Notifierar observat�rer om att en ny produkt har lagts till

            return Ok();
        }
    }
}
