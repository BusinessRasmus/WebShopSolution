using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Models;

namespace WebShop.Infrastructure.Notifications.Observers
{
    public class PushMessageSenderObserver : INotificationObserver<Product>
    {
        public void Update(Product product)
        {
            // Placeholder för logik att skicka en push-notis. Skriver ut till konsolen för enkelhetens skull.
            Console.WriteLine($"Push notification to everyone: New product added - {product.Name}");
        }
    }
}
