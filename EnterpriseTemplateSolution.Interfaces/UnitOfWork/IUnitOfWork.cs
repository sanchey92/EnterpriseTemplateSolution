using EnterpriseTemplateSolution.Entities.Base;
using EnterpriseTemplateSolution.Interfaces.Repositories;

namespace EnterpriseTemplateSolution.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync();
}