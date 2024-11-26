using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Subjects;

namespace WebShop.Infrastructure.Notifications.Factory
{
    public class SubjectFactory : ISubjectFactory
    {
        public ISubject<TEntity> CreateSubject<TEntity>() where TEntity : class
        {
            if (typeof(TEntity) == typeof(Product))
                    return (ISubject<TEntity>)new ProductSubject();

                return new Subject<TEntity>();
        }
    }
}
