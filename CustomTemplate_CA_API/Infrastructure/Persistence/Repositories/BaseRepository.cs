using CustomTemplate_CA_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomTemplate_CA_API.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<E>(IDbContextFactory<AppDatabaseContext> dbContextFactory)
    : IBaseRepository<E> where E : class
{
    protected readonly IDbContextFactory<AppDatabaseContext> _dbContextFactory = dbContextFactory;
    protected DbSet<E> GetDbSet(AppDatabaseContext context) => context.Set<E>();

    public async virtual Task<IEnumerable<E>?> GetAllAsync()
    {
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Set<E>().ToListAsync();
    }

    public async virtual Task<E?> FindByIdAsync(Guid id)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        return await context.Set<E>().FindAsync(id);
    }

    public async virtual Task<E?> FindByFiltersAsync(Dictionary<string, object> filters)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        IQueryable<E> query = context.Set<E>();

        var parameter = Expression.Parameter(typeof(E), "e");
        Expression finalExpression = Expression.Constant(true);

        foreach (var filter in filters)
        {
            var property = Expression.Property(parameter, filter.Key);
            var value = Expression.Constant(filter.Value);
            var equals = Expression.Equal(property, value);
            finalExpression = Expression.AndAlso(finalExpression, equals);
        }

        var lambda = Expression.Lambda<Func<E, bool>>(finalExpression, parameter);
        return await query.Where(lambda).FirstOrDefaultAsync();
    }

    public async virtual Task AddAsync(E entity)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        context.Set<E>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async virtual Task UpdateAsync(E entity)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        context.Set<E>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async virtual Task DeleteByIdAsync(Guid id)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        var entity = await context
            .Set<E>()
            .FindAsync(id)
            ?? throw new KeyNotFoundException($"Entity of type {typeof(E).Name} with ID {id} not found.");
        context.Set<E>().Remove(entity);
        await context.SaveChangesAsync();
    }
}
