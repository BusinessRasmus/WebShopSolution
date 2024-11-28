using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    public class OrderDetail : IModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
