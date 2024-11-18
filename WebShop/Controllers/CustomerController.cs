using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Customer>> GetCustomer()
        {
            // Behöver använda repository via Unit of Work för att hämta produkter
            return Ok();
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Add(customer);
            // Lägger till produkten via repository

            // Sparar förändringar

            // Notifierar observatörer om att en ny produkt har lagts till

            return Ok();
        }
    }
}
