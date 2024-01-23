using B1TestTask.UI.Data.Repositories.Base;
using B1TestTask.UI.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B1TestTask.UI.Data.Repositories;
internal class EfRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DataContext _context;

    public EfRepository(DataContext context) => _context = context;

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression);

    public IQueryable<T> GetAll() => _context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Set<T>().Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

internal class BankRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly BankContext _context;

    public BankRepository(BankContext context) => _context = context;

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression);

    public IQueryable<T> GetAll() => _context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Set<T>().Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
