namespace WebShop.Infrastructure.Notifications.Observers
{
    public interface INotificationObserver<T> where T : class
    {
        void Update(T entity); 
    }

}
