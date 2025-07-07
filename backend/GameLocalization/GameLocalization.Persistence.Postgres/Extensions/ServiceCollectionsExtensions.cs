using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Persistence.Postgres.Connection;
using GameLocalization.Persistence.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameLocalization.Persistence.Postgres.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IKeysRepository, KeysRepository>();
        services.AddScoped<ILanguagesRepository, LanguagesRepository>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<ITranslationsRepository, TranslationsRepository>();
        services.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        
        return services;
    }
}