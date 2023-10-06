using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Dal.Infrastructure;
using Workshop.Dal.Settings;

namespace Workshop.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.Configure<DalOptions>(configuration.GetSection(nameof(DalOptions)));

        Postgres.MapCompositeTypes();

        Postgres.AddMigrations(services);

        return services;
    }
}