using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;

namespace WebShop.Infrastructure.Notifications.Subjects
{
    // Subject för Products som håller reda på observatörer och notifierar dem
    public class ProductSubject : ISubject<Product>
    {
        // Observers
        private readonly List<INotificationObserver<Product>> _observers = [];

        private static ProductSubject? _productSubject;

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

        public void Notify(Product product)
        {
            foreach (var observer in _observers)
            {
                observer.Update(product);
            }
        }
    }
}
