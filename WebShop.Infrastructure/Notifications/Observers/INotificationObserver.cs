using WebShop.Domain.Interfaces;

namespace WebShop.Infrastructure.Notifications.Observers
{
    // Gränssnitt för notifieringsobservatörer enligt Observer Pattern
    public interface INotificationObserver<T> where T : class
    {
        //TODO Gör generisk? För att uppfylla O-C-P
        void Update(T entity); // Metod som kallas när en ny produkt läggs till
    }

}
