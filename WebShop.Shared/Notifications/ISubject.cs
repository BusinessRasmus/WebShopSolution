using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebShop.Shared.Models;

namespace WebShop.Shared.Notifications
{
    public interface ISubject<TEntity> where TEntity : IModel
    {
        void Attach(INotificationObserver<TEntity> observer);
        void Detach(INotificationObserver<TEntity> observer);
        void Notify(TEntity entity);
    }
}
