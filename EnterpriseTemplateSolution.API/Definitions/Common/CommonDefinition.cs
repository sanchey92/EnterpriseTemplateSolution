using EnterpriseTemplateSolution.API.Definitions.Base;

namespace EnterpriseTemplateSolution.API.Definitions.Common;

public class CommonDefinition : ApplicationDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
    }

    public override void ConfigureApplication(WebApplication application, IWebHostEnvironment environment)
    {
        application.UseStaticFiles();
        application.UseAuthentication();
        application.UseRouting();
        application.UseAuthorization();
        application.MapControllers();
        application.Run();
    }
}