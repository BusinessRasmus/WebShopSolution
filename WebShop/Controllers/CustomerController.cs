//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Identity.Client;
//using System.ComponentModel.DataAnnotations;
//using WebShop.DataAccess.Repositories;
//using WebShop.DataAccess.UnitOfWork;
//using WebShop.Shared.Models;
//using WebShop.Shared.Notifications;

//namespace WebShop.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class CustomerController : ControllerBase
//    {

//        private readonly IUnitOfWork _unitOfWork;

//        public CustomerController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
//        {
//            var repository = await _unitOfWork.Repository<Customer>();
//            var customers = await repository.GetAllAsync();

//            if (!customers.Any())
//                return NotFound(Enumerable.Empty<Customer>());

//            return Ok(customers);
//        }

//        // Endpoint för att hämta alla produkter
//        [HttpGet]
//        public async Task<ActionResult<Customer>> GetCustomerById(int id)
//        {
//            var repository = await _unitOfWork.Repository<Customer>();
//            var customer = await repository.GetByIdAsync(id);

//            ProductSubject productSubject = new ProductSubject();

//            if (customer is null)
//            {
//                return NotFound($"No customer with id {id} was found.");
//            }

//            return Ok(customer);
//        }

//        // Endpoint för att lägga till en ny produkt
//        [HttpPost]
//        public async Task<ActionResult> AddCustomer(Customer customer)
//        {
//            if (customer.FirstName.Contains('<'))
//            {
//                return BadRequest("Forbidden characters in request");
//            }

//            try
//            {
//                var repository = await _unitOfWork.Repository<Customer>();
//                await repository.AddAsync(customer);
//                await _unitOfWork.Complete();
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, $"Internal server error: {e.Message}");
//            }
//            return Ok();
//        }

//        [HttpPatch]
//        [Route("{id}")]
//        public async Task<ActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
//        {
//            if (!Validator.TryValidateObject(customer, new ValidationContext(customer), null, true))
//            {
//                return BadRequest();
//            }

//            if (customer is null)
//                return BadRequest();

//            var repository = await _unitOfWork.Repository<Customer>();

//            await repository.UpdateAsync(customer);
//            await _unitOfWork.Complete();

//            return Ok();
//        }

//        [HttpDelete]
//        [Route("{id}")]
//        public async Task<ActionResult> DeleteCustomer([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest();

//            var customerToDelete = await _unitOfWork.Repository<Customer>().Result.GetByIdAsync(id);

//            if (customerToDelete is null)
//                return NotFound($"No customer with id {id} was found.");

//            try
//            {
//                await _unitOfWork.Repository<Customer>().Result.DeleteAsync(customerToDelete.Id);
//                await _unitOfWork.Complete();
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, $"Internal server error: {e.Message}");
//            }

//            return Ok();
//        }
//    }
//}
