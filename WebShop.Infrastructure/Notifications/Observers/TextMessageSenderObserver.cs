using WebShop.Domain.Models;

namespace WebShop.Infrastructure.Notifications.Observers
{
    public class TextMessageSenderObserver : INotificationObserver<Product>
    {
        public void Update(Product product)
        {
            // Placeholder för logik att skicka ett SMS. Skriver ut till konsolen för enkelhetens skull.
            Console.WriteLine($"Text message notification to everyone: New product added - {product.Name}");
        }
    }
}
