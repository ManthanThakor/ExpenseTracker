using System.Linq.Expressions;

namespace ExpenseTrackerMvc.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T?> GetById(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> Exists(int id);
        Task<int> Count(Expression<Func<T, bool>>? predicate = null);
        Task SaveChanges();
    }
}
