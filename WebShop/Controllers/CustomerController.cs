using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;

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

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var repository = await _unitOfWork.Repository<Customer>();
            var response = await repository.GetByIdAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public async Task<ActionResult> Add(Customer customer)
        {
            var repository = await _unitOfWork.Repository<Customer>();
            await repository.AddAsync(customer);

            return Ok();
        }
    }
}
