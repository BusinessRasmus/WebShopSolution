using System.ComponentModel.DataAnnotations;
using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    public class Order : IModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public required string CustomerFirstName { get; set; }

        [Required]
        public string OrderStatus { get; set; } = "Pending";

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
