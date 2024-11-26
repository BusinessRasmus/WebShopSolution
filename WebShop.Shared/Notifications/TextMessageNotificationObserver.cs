using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Shared.Models;

namespace WebShop.Shared.Notifications
{
    public class TextMessageNotificationObserver : INotificationObserver<Product>
    {
        public void Update(Product product)
        {
            // Placeholder för logik att skicka ett SMS. Skriver ut till konsolen för enkelhetens skull.
            Console.WriteLine($"Text message notification: New product added - {product.Name}");
        }
    }
}
