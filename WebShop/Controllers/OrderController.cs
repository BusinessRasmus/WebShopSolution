using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var repository = await _unitOfWork.Repository<Order>();
            var order = await repository.GetAllAsync();

            if (!order.Any())
                return NotFound(Enumerable.Empty<Order>());

            return Ok(order);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderById([FromRoute] int id)
        {
            var repository = await _unitOfWork.Repository<Order>();
            var order = await repository.GetByIdAsync(id);

            if (order is null)
                return NotFound($"No products were found.");

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] Order order)
        {
            if (!Validator.TryValidateObject(order, new ValidationContext(order), null, true))
            {
                return BadRequest();
            }

            if (order is null)
                return BadRequest();

            var repository = await _unitOfWork.Repository<Order>();

            await repository.AddAsync(order);
            await _unitOfWork.Complete();

            //TODO Lägg in notification här.
            // Notifierar observatörer om att en ny produkt har lagts till

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Order order)
        {
            if (!Validator.TryValidateObject(order, new ValidationContext(order), null, true))
            {
                return BadRequest();
            }

            if (order is null)
                return BadRequest();

            var repository = await _unitOfWork.Repository<Order>();

            await repository.UpdateAsync(order);
            await _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderToDelete = await _unitOfWork.Repository<Order>().Result.GetByIdAsync(id);

            if (orderToDelete is null)
                return NotFound($"No product with id {id} was found.");

            try
            {
                await _unitOfWork.Repository<Order>().Result.DeleteAsync(orderToDelete.Id);
                await _unitOfWork.Complete();
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
