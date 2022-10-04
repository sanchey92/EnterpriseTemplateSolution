using EnterpriseTemplateSolution.DataAccess.DbContexts;
using EnterpriseTemplateSolution.DataAccess.Specifications;
using EnterpriseTemplateSolution.Entities.Base;
using EnterpriseTemplateSolution.Interfaces.Repositories;
using EnterpriseTemplateSolution.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTemplateSolution.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyList<TEntity>> GetListAsync(bool trackChanges)
    {
        return trackChanges
            ? await _dbContext.Set<TEntity>().ToListAsync()
            : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetEntityByIdAsync(Guid id, bool trackChanges)
    {
        return trackChanges
            ? await _dbContext.Set<TEntity>().FirstOrDefaultAsync()
            : await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetListAsyncWithSpec(ISpecification<TEntity> specification,
        bool trackChanges)
    {
        return trackChanges
            ? await ApplySpecification(specification).ToListAsync()
            : await ApplySpecification(specification).AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetEntityWithSpecAsync(ISpecification<TEntity> specification, bool trackChanges)
    {
        return trackChanges
            ? await ApplySpecification(specification).FirstOrDefaultAsync()
            : await ApplySpecification(specification).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
    }
}