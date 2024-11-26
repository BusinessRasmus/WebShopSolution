using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure.Notifications.Factory;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShop.Infrastructure.Notifications.SubjectManager
{
    public class SubjectManager(ISubjectFactory factory) : ISubjectManager
    {
        //TODO Tester
        public ISubject<TEntity> Subject<TEntity>() where TEntity : class
        {
            var subject =  factory.CreateSubject<TEntity>();
            return subject;
        }
    }
}
