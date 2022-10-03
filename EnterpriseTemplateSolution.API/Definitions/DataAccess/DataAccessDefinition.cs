using EnterpriseTemplateSolution.API.Definitions.Base;
using EnterpriseTemplateSolution.DataAccess;

namespace EnterpriseTemplateSolution.API.Definitions.DataAccess;

public class DataAccessDefinition : ApplicationDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessLayer(configuration);
    }
}