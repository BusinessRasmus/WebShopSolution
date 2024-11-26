using WebShop.Shared.Models;
using WebShop.Shared.Notifications.Observers;

namespace WebShop.Shared.Notifications.Subjects
{
    // Subject som håller reda på observatörer och notifierar dem
    public class ProductSubject : ISubject<Product>
    {

        // Lista över registrerade observatörer
        private readonly List<INotificationObserver<Product>> _observers = new List<INotificationObserver<Product>>();

        public void Attach(INotificationObserver<Product> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INotificationObserver<Product> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(Product product) //TODO Add abstraction here?
        {
            // Notifiera alla observatörer om en ny produkt
            foreach (var observer in _observers)
            {
                observer.Update(product);
            }
        }
    }
}
