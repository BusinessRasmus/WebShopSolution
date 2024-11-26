
using WebShop.Shared.Models;

namespace WebShop.Shared.Notifications
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
