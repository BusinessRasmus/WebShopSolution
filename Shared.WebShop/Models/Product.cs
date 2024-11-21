using System.ComponentModel.DataAnnotations;

namespace WebShop.Shared.Models
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product : IModel
    {
        public int Id { get; set; } // Unikt ID f�r produkten

        [Required]
        [Length(2, 30)]
        public string Name { get; set; } // Namn p� produkten

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        public ICollection<OrderItem> OrderProducts { get; set; } = new List<OrderItem>();
    }
}