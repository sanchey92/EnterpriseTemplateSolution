using System.Collections;
using EnterpriseTemplateSolution.DataAccess.DbContexts;
using EnterpriseTemplateSolution.Entities.Base;
using EnterpriseTemplateSolution.Interfaces.Repositories;
using EnterpriseTemplateSolution.Interfaces.UnitOfWork;

namespace EnterpriseTemplateSolution.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Dispose() => _dbContext.Dispose();

    public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        _repositories ??= new Hashtable();
        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type)) return (IGenericRepository<TEntity>)_repositories[type];

        var repositoryType = typeof(GenericRepository<>);

        var repositoryInstance =
            Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

        _repositories.Add(type, repositoryInstance);

        return (IGenericRepository<TEntity>)_repositories[type];
    }
}