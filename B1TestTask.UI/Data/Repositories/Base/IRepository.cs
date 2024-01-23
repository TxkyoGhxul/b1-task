using B1TestTask.UI.Models.Base;
using System.Linq.Expressions;

namespace B1TestTask.UI.Data.Repositories.Base;
internal interface IRepository<T> where T : class, IEntity, new()
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    IQueryable<T> Get(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
}