using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;

namespace WebShop.Infrastructure.Notifications.Subjects
{
    // Subject som håller reda på observatörer och notifierar dem
    public class ProductSubject : ISubject<Product>
    {
        // Lista över registrerade observatörer
        private readonly List<INotificationObserver<Product>> _observers = [];

        private static ProductSubject _productSubject;

        public static ProductSubject Instance
        {
            get
            {
                if (_productSubject == null)
                    _productSubject = new ProductSubject();

                return _productSubject;
            }
        }

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
