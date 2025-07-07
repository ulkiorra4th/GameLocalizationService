using GameLocalization.Infrastructure.Mapping.Mappers;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace GameLocalization.Infrastructure.Mapping.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddSingleton<IKeyMapper, KeyMapper>();
        services.AddSingleton<ILanguageMapper, LanguageMapper>();
        services.AddSingleton<IProjectMapper, ProjectMapper>();
        services.AddSingleton<ITranslationMapper, TranslationMapper>();

        return services;
    }
}