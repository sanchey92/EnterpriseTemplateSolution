using Microsoft.EntityFrameworkCore;

namespace EnterpriseTemplateSolution.DataAccess.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}