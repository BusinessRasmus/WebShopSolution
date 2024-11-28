using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebShop.Domain.Models;
using WebShop.Infrastructure.UnitOfWork;

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
            var repository = _unitOfWork.Repository<Order>();
            var orders = await repository.GetAllAsync();

            var orderDetailsRepository = _unitOfWork.Repository<OrderDetail>();
            var orderDetails = await orderDetailsRepository.GetAllAsync();

            if (!orders.Any())
                return NotFound(Enumerable.Empty<Order>());

            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderById([FromRoute] int id)
        {
            var repository = _unitOfWork.Repository<Order>();
            var order = await repository.GetByIdAsync(id);

            if (order is null)
                return NotFound($"No order with id {id} was found.");

            var orderDetailRepository = _unitOfWork.Repository<OrderDetail>();
            var orderDetails = await orderDetailRepository.GetAllAsync();

            order.OrderDetails = orderDetails.Where(od => od.OrderId == order.Id).ToList();

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

            var repository = _unitOfWork.Repository<Order>();

            await repository.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!Validator.TryValidateObject(order, new ValidationContext(order), null, true))
            {
                return BadRequest();
            }

            if (order is null)
                return BadRequest();

            var repository = _unitOfWork.Repository<Order>();

            repository.Update(order);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var repo = _unitOfWork.Repository<Order>();
            var orderToDelete = repo.GetByIdAsync(id);

            if (orderToDelete is null)
                return NotFound($"No order with id {id} was found.");

            var orderDetailsRepo = _unitOfWork.Repository<OrderDetail>();
            var allOrderDetails = await orderDetailsRepo.GetAllAsync();
            var orderDetailsToDelete = allOrderDetails.Where(od => od.OrderId == id);

            try
            {
                foreach (var orderDetail in orderDetailsToDelete)
                {
                    await orderDetailsRepo.DeleteAsync(orderDetail.Id);
                }

                await repo.DeleteAsync(orderToDelete.Id);
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
