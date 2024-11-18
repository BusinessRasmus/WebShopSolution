
namespace WebShop.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T item);
        Task Update(T item);
        Task Remove(int id);
    }
}
