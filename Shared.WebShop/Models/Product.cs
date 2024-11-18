namespace WebShop.Shared.Models
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product : IModel
    {
        public int Id { get; set; } // Unikt ID för produkten
        public string Name { get; set; } // Namn på produkten
        public double Price { get; set; }
        public int Amount { get; set; }

        public ICollection<OrderItem> OrderProducts { get; set; } = new List<OrderItem>();
    }
}