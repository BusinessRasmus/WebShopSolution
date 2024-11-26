using WebShop.Shared.Models;

namespace WebShop.Shared.Notifications.Observers
{
    // Gränssnitt för notifieringsobservatörer enligt Observer Pattern
    public interface INotificationObserver<T> where T : IModel
    {
        //TODO Gör generisk? För att uppfylla O-C-P
        void Update(T entity); // Metod som kallas när en ny produkt läggs till
    }

}
