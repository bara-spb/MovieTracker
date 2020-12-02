using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using bMovieTracker.Domain;
using bMovieTracker.App;
using Microsoft.EntityFrameworkCore;

namespace bMovieTracker.Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MovieTrackerDbContext _dbContext;

        public EFRepository(MovieTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetById(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> GetAll(CancellationToken ct = default(CancellationToken))
        {
            return await _dbContext.Set<T>().ToListAsync(ct);
        }

        public virtual async Task<T> Add(T entity, CancellationToken ct = default(CancellationToken))
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync(ct);
            return entity;
        }

        public virtual async Task<bool> Update(T entity, CancellationToken ct = default(CancellationToken))
        {
            if (entity == null)
                return false;
            if (!await EntityExists(entity.Id, ct))
                return false;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }

        public virtual async Task<bool> Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await EntityExists(id, ct))
                return false;
            var toRemove = _dbContext.Find<T>(id);
            _dbContext.Set<T>().Remove(toRemove);
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }

        private async Task<bool> EntityExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetById(id, ct) != null;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
