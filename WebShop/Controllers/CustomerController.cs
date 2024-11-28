using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebShop.Domain.Models;
using WebShop.Infrastructure.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var repository = _unitOfWork.Repository<Customer>();
            var customers = await repository.GetAllAsync();

            if (!customers.Any())
                return NotFound(Enumerable.Empty<Customer>());

            return Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById([FromRoute] int id)
        {
            var repository = _unitOfWork.Repository<Customer>();
            var customer = await repository.GetByIdAsync(id);

            if (customer is null)
            {
                return NotFound($"No customer with id {id} was found.");
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer.FirstName.Contains('<'))
            {
                return BadRequest("Forbidden characters in request");
            }

            try
            {
                var repository = _unitOfWork.Repository<Customer>();
                await repository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!Validator.TryValidateObject(customer, new ValidationContext(customer), null, true))
            {
                return BadRequest();
            }

            if (customer is null)
                return BadRequest();

            var repository = _unitOfWork.Repository<Customer>();

            repository.Update(customer);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var repo = _unitOfWork.Repository<Customer>();
            var customerToDelete = await repo.GetByIdAsync(id);

            if (customerToDelete is null)
                return NotFound($"No customer with id {id} was found.");

            try
            {
                await repo.DeleteAsync(customerToDelete.Id);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }

            return Ok();
        }
    }
}
