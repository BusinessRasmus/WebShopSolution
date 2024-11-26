using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebShop.Shared.Models
{
    public class Order : IModel
    {
        public int Id { get; set; }

        [Required]
        public required Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem> OrderProducts { get; set; } = new List<OrderItem>();

    }
}
