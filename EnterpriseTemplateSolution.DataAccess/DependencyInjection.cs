using EnterpriseTemplateSolution.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace EnterpriseTemplateSolution.DataAccess;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultDbConnection = GenerateConnectionString(configuration, "DefaultConnection");
        var identityDbConnection = GenerateConnectionString(configuration, "IdentityConnection");

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(defaultDbConnection));
        services.AddDbContext<ApplicationIdentityDbContext>(opt => opt.UseNpgsql(identityDbConnection));
    }

    private static string GenerateConnectionString(IConfiguration configuration, string name)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            ConnectionString = configuration.GetConnectionString(name),
            Username = configuration["User ID"],
            Password = configuration["Password"]
        };

        return builder.ConnectionString;
    }
}