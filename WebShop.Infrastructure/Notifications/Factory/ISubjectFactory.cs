using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShop.Infrastructure.Notifications.Factory
{
    public interface ISubjectFactory
    {
        public ISubject<TEntity> CreateSubject<TEntity>() where TEntity : class;
    }
}
