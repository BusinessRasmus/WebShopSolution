using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShop.Infrastructure.Notifications.Factory
{
    internal interface ISubjectFactory
    {
        public ISubject<TEntity> CreateSubject<TEntity>() where TEntity : class;
    }
}
