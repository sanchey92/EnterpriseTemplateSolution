using EnterpriseTemplateSolution.DataAccess.Configuration;
using EnterpriseTemplateSolution.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTemplateSolution.DataAccess.DbContexts;

public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
    }
}