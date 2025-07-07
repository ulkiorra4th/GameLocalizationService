using GameLocalization.Application.Services;
using GameLocalization.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameLocalization.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IKeysService, KeysService>();
        services.AddScoped<ILanguagesService, LanguagesService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<ITranslationsService, TranslationsService>();

        return services;
    }
    
    
}