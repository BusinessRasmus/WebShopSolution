using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Interfaces;
using WebShop.Infrastructure.Notifications.Observers;

namespace WebShop.Infrastructure.Notifications.Subjects
{
    public interface ISubject<TEntity> where TEntity : class
    {
        void Attach(INotificationObserver<TEntity> observer);
        void Detach(INotificationObserver<TEntity> observer);
        void Notify(TEntity entity);
    }
}
