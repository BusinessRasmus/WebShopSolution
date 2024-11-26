using WebShop.Domain.Models;

namespace WebShop.Infrastructure.Notifications.Observers
{
    // En konkret observatör som skickar e-postmeddelanden
    public class EmailNotificationObserver : INotificationObserver<Product>
    {
        public void Update(Product product)
        {
            // Placeholder för logik att skicka ett SMS. Skriver ut till konsolen för enkelhetens skull.
            Console.WriteLine($"Email notification: New product added - {product.Name}");
        }

    }
}
