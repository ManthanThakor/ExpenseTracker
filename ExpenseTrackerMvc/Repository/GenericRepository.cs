using ExpenseTrackerMvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTrackerMvc.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChanges();
            return entity;
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetById(id);
            return entity != null;
        }

        public async Task<int> Count(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
            {
                return await _dbSet.CountAsync();
            }
            return await _dbSet.CountAsync(predicate);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}


