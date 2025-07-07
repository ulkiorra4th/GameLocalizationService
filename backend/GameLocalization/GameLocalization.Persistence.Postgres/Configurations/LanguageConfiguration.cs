using GameLocalization.Persistence.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLocalization.Persistence.Postgres.Configurations;

internal sealed class LanguageConfiguration : IEntityTypeConfiguration<LanguageEntity>
{
    private static readonly List<LanguageEntity> Languages =
    [
        new()
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Code = "ru",
            Name = "Russian",
            IsCustom = false
        },

        new()
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Code = "en",
            Name = "English",
            IsCustom = false
        },

        new()
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Code = "tr",
            Name = "Turkish",
            IsCustom = false
        }
    ];
    
    public void Configure(EntityTypeBuilder<LanguageEntity> builder)
    {
        builder.ToTable("language");
        
        builder.HasOne(l => l.ProjectCreated)
            .WithMany(p => p.CustomLanguages!)
            .HasForeignKey(l => l.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.UsingProjects)
            .WithMany(p => p.LanguagesInTable)
            .UsingEntity<LanguageInTableEntity>(e =>
            {
                e.ToTable("language_in_table");
                e.HasData(new List<LanguageInTableEntity>
                {
                    new()
                    {
                        ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        LanguageId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                    },
                    new()
                    {
                        ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        LanguageId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                    },
                    new()
                    {
                        ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        LanguageId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                    }
                });
            });
        
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Code).IsRequired().HasMaxLength(3);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
        builder.Property(l => l.IsCustom).IsRequired();
        
        builder.HasData(Languages);
    }
}