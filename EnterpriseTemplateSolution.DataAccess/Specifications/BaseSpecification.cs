using System.Linq.Expressions;
using EnterpriseTemplateSolution.Interfaces.Specifications;

namespace EnterpriseTemplateSolution.DataAccess.Specifications;

public class BaseSpecification<TEntity> : ISpecification<TEntity>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria) => Criteria = criteria;
    
    public Expression<Func<TEntity, bool>> Criteria { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
    public Expression<Func<TEntity, object>> OrderBy { get; private set; }
    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> expression) => Includes.Add(expression);
    
    protected void AddOrderBy(Expression<Func<TEntity, object>> expression) => OrderBy = expression;
    
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression) => OrderByDescending = expression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = Take;
        IsPagingEnabled = true;
    }
}