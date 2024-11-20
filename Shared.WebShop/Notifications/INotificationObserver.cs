using WebShop.Shared.Models;

namespace WebShop.Shared.Notifications
{
    // Gränssnitt för notifieringsobservatörer enligt Observer Pattern
    public interface INotificationObserver
    {
        //TODO Gör generisk? För att uppfylla O-C-P
        void Update(Product product); // Metod som kallas när en ny produkt läggs till
    }
}
