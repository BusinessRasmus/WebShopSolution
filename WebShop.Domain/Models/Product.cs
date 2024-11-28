using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product : IModel
    {
        public int Id { get; set; }

        [Required]
        [Length(2, 30)]
        public required string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}