using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PebriBox.Application.Features.Agents;
using PebriBox.Application.Features.Properties;
using PebriBox.Infrastructure.Services;

namespace PebriBox.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<Contexts.ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsHistoryTable("Migrations", "EFCore"))
        ).AddScoped<IAgentService, AgentService>()
        .AddScoped<IPropertyService, PropertyService>();
    }
}
