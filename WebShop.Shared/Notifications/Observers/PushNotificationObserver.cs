using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Shared.Models;

namespace WebShop.Shared.Notifications.Observers
{
    public class PushNotificationObserver : INotificationObserver<Product>
    {
        //DETTA ÄR VÅR OBSERVER
        public void Update(Product product)
        {
            // Placeholder för logik att skicka en push-notis. Skriver ut till konsolen för enkelhetens skull.
            Console.WriteLine($"Push notification: New product added - {product.Name}");
        }
    }
}
