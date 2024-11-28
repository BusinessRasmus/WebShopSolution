using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    public class OrderDetail : IModel
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
