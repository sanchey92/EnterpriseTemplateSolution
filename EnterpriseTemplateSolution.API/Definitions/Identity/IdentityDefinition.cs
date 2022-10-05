using EnterpriseTemplateSolution.API.Definitions.Base;
using EnterpriseTemplateSolution.DataAccess.DbContexts;
using EnterpriseTemplateSolution.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseTemplateSolution.API.Definitions.Identity;

public class IdentityDefinition : ApplicationDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication();
    }
}