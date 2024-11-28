using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    public class Order : IModel
    {
        public int Id { get; set; }

        [Required]
        public required string CustomerFirstName { get; set; }

        [Required]
        public string OrderStatus { get; set; } = "Pending";

        [JsonIgnore]
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
