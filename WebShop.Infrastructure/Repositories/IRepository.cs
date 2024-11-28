namespace WebShop.Infrastructure.Repositories
{
    public interface IRepository<TE> where TE : class
    {
        Task<TE> GetByIdAsync(int id);
        Task<IEnumerable<TE>> GetAllAsync();
        Task AddAsync(TE item);
        public void Update(TE item);
        Task DeleteAsync(int id);
    }
}
