using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;
using WebShop.Infrastructure.Notifications.SubjectManager;
using WebShop.Infrastructure.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectManager _subjectManager;

        public ProductController(IUnitOfWork unitOfWork, ISubjectManager subjectManager)
        {
            _unitOfWork = unitOfWork;
            _subjectManager = subjectManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var repository = _unitOfWork.Repository<Product>();
            var products = await repository.GetAllAsync();

            if (!products.Any())
                return NotFound(Enumerable.Empty<Product>());

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById([FromRoute] int id)
        {
            var repository = _unitOfWork.Repository<Product>();
            var product = await repository.GetByIdAsync(id);

            if (product is null)
                return NotFound($"No product with id {id} was found.");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            if (!Validator.TryValidateObject(product, new ValidationContext(product), null, true))
            {
                return BadRequest();
            }

            if (product is null)
                return BadRequest();

            var repository = _unitOfWork.Repository<Product>();
            await repository.AddAsync(product);

            await _unitOfWork.CompleteAsync();

            var subject = _subjectManager.Subject<Product>();
            subject.Attach(new EmailSenderObserver());
            subject.Attach(new TextMessageSenderObserver());
            subject.Attach(new PushMessageSenderObserver());

            subject.Notify(product);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!Validator.TryValidateObject(product, new ValidationContext(product), null, true))
            {
                return BadRequest();
            }

            if (product is null)
                return BadRequest();

            var repository = _unitOfWork.Repository<Product>();
            repository.Update(product);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var repo = _unitOfWork.Repository<Product>();
            var productToDelete = await repo.GetByIdAsync(id);

            if (productToDelete is null)
                return NotFound($"No product with id {id} was found.");

            try
            {
                await repo.DeleteAsync(productToDelete.Id);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                _unitOfWork.Dispose();
                return StatusCode(500, $"Internal server error: {e.Message}");
            }

            return Ok();
        }
    }
}
