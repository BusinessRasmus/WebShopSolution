using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure.Notifications.Factory;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShop.Infrastructure.Notifications.SubjectManager
{
    public class SubjectManager(SubjectFactory factory)
    {
        public ISubject<TEntity> Subject<TEntity>() where TEntity : class
        {
            var subject =  factory.CreateSubject<TEntity>();
            return subject;
        }
    }
}
