namespace CustomTemplate_CA_API.Core.Repositories;

public interface IBaseRepository
{
    public Task<IEnumerable<TEntity>?> GetAllAsync<TEntity>() where TEntity : class;
    public Task<TEntity?> FindByIdAsync<TEntity>(Guid id) where TEntity : class;
    public Task<TEntity?> FindByFiltersAsync<TEntity>(Dictionary<string, object> filters) where TEntity : class;
    public Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
    public Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
    public Task DeleteByIdAsync<TEntity>(Guid id) where TEntity : class;
}
