namespace CustomTemplate_CA_API.Core.Repositories;

public interface IBaseRepository<E> where E : class
{
    public Task<IEnumerable<E>?> GetAllAsync();
    public Task<E?> FindByIdAsync(Guid id);
    public Task<E?> FindByFiltersAsync(Dictionary<string, object> filters);
    public Task AddAsync(E entity);
    public Task UpdateAsync(E entity);
    public Task DeleteByIdAsync(Guid id);
}
