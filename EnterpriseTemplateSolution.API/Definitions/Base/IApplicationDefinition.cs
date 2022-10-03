namespace EnterpriseTemplateSolution.API.Definitions.Base;

public interface IApplicationDefinition
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    void ConfigureApplication(WebApplication application, IWebHostEnvironment environment);
}