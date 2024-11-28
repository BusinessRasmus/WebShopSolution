using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShop.Infrastructure.Notifications.SubjectManager
{
    public interface ISubjectManager
    {
        public ISubject<TEntity> Subject<TEntity>() where TEntity : class;
    }
}
