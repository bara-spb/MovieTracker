using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bMovieTracker.Domain;

namespace bMovieTracker.App
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id, CancellationToken ct = default(CancellationToken));
        IQueryable<T> GetAllAsQueryable();
        Task<IReadOnlyList<T>> GetAll(CancellationToken ct = default(CancellationToken));
        Task<T> Add(T entity, CancellationToken ct = default(CancellationToken));
        Task<bool> Update(T entity, CancellationToken ct = default(CancellationToken));
        Task<bool> Delete(int id, CancellationToken ct = default(CancellationToken));
    }
}
