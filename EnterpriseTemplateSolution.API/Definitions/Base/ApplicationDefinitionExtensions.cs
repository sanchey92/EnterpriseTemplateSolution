namespace EnterpriseTemplateSolution.API.Definitions.Base;

public static class ApplicationDefinitionExtensions
{
    public static void AddDefinitions(this IServiceCollection services, WebApplicationBuilder builder,
        params Type[] entryPointsAssembly)
    {
        var definitions = new List<IApplicationDefinition>();

        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(x => 
                !x.IsAbstract && typeof(IApplicationDefinition).IsAssignableFrom(x));

            var instances = types.Select(Activator.CreateInstance).Cast<IApplicationDefinition>();
            
            definitions.AddRange(instances);
        }
        definitions.ForEach(app => app.ConfigureServices(services, builder.Configuration));
        services.AddSingleton(definitions as IReadOnlyCollection<IApplicationDefinition>);
    }

    public static void UseDefinitions(this WebApplication application)
    {
        var definitions = application.Services.GetRequiredService<IReadOnlyCollection<IApplicationDefinition>>();
        var environment = application.Services.GetRequiredService<IWebHostEnvironment>();

        foreach (var definition in definitions)
            definition.ConfigureApplication(application, environment);
    }
}