using EnterpriseTemplateSolution.API.Definitions.Base;
using EnterpriseTemplateSolution.Services;
using EnterpriseTemplateSolution.Services.Interfaces;

namespace EnterpriseTemplateSolution.API.Definitions.Services;

public class ServicesDefinition : ApplicationDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }
}