using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Interfaces;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;

namespace WebShop.Infrastructure.Notifications.Subjects
{
    public class Subject<TEntity> : ISubject<TEntity> where TEntity : class
    {
        // Lista över registrerade observatörer
        private readonly List<INotificationObserver<TEntity>> _observers = [];

        public void Attach(INotificationObserver<TEntity> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INotificationObserver<TEntity> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(TEntity product) //TODO Add abstraction here?
        {
            // Notifiera alla observatörer om en ny produkt
            foreach (var observer in _observers)
            {
                observer.Update(product);
            }
        }
    }
}
