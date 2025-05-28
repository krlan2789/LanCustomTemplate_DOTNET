using CustomTemplate_CA_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomTemplate_CA_API.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository(AppDatabaseContext dbContext)
    : IBaseRepository
{
    protected readonly AppDatabaseContext _dbContext = dbContext;
    protected DbSet<TEntity> GetDbSet<TEntity>(AppDatabaseContext context) where TEntity : class => context.Set<TEntity>();

    public async virtual Task<IEnumerable<TEntity>?> GetAllAsync<TEntity>() where TEntity : class
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async virtual Task<TEntity?> FindByIdAsync<TEntity>(Guid id) where TEntity : class
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async virtual Task<TEntity?> FindByFiltersAsync<TEntity>(Dictionary<string, object> filters) where TEntity : class
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        Expression finalExpression = Expression.Constant(true);

        foreach (var filter in filters)
        {
            var property = Expression.Property(parameter, filter.Key);
            var value = Expression.Constant(filter.Value);
            var equals = Expression.Equal(property, value);
            finalExpression = Expression.AndAlso(finalExpression, equals);
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(finalExpression, parameter);
        return await query.Where(lambda).FirstOrDefaultAsync();
    }

    public async virtual Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async virtual Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async virtual Task DeleteByIdAsync<TEntity>(Guid id) where TEntity : class
    {
        var entity = await _dbContext
            .Set<TEntity>()
            .FindAsync(id)
            ?? throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {id} not found.");
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
