using EnterpriseTemplateSolution.Entities.Base;
using EnterpriseTemplateSolution.Interfaces.Specifications;

namespace EnterpriseTemplateSolution.Interfaces.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetListAsync(bool trackChanges); 
    Task<IReadOnlyList<TEntity>> GetListAsyncWithSpec(ISpecification<TEntity> specification, bool trackChanges);
    Task<TEntity?> GetEntityByIdAsync(Guid id, bool trackChanges); 
    Task<TEntity> GetEntityWithSpecAsync(ISpecification<TEntity> specification, bool trackChanges);
    Task<int> CountAsync(ISpecification<TEntity> specification);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}