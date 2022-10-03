namespace EnterpriseTemplateSolution.API.Definitions.Base;

public abstract class ApplicationDefinition : IApplicationDefinition
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public virtual void ConfigureApplication(WebApplication application, IWebHostEnvironment environment)
    {
    }
}