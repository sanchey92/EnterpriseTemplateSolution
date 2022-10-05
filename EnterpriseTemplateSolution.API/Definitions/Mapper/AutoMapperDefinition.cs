using EnterpriseTemplateSolution.API.Definitions.Base;

namespace EnterpriseTemplateSolution.API.Definitions.Mapper;

public class AutoMapperDefinition : ApplicationDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
    }
}