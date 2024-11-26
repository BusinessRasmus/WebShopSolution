using WebShop.Domain.Models;

namespace WebShop.Infrastructure.Notifications.Observers
{
    // En konkret observatör som skickar e-postmeddelanden
    public class EmailSenderObserver : INotificationObserver<Product>
    {
        //TODO Tester?
        public void Update(Product product)
        {
            // Placeholder för logik att skicka ett SMS. Skriver ut till konsolen för enkelhetens skull.
            Console.WriteLine($"Email notification to everyone: New product added - {product.Name}");
        }

    }
}
