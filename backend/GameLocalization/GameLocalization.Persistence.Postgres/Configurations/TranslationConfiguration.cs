using GameLocalization.Persistence.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLocalization.Persistence.Postgres.Configurations;

internal sealed class TranslationConfiguration : IEntityTypeConfiguration<TranslationEntity>
{
    private static readonly List<TranslationEntity> Translations =
    [
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Value = "Меню"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Value = "Menu"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Value = "Menu"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
            Value = "Очки"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
            Value = "Score"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111113"),
            Value = "Настройки"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111113"),
            Value = "Settings"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111114"),
            Value = "Предмет"
        },
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            LanguageId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            KeyId = Guid.Parse("11111111-1111-1111-1111-111111111114"),
            Value = "Item"
        }
    ];
    
    public void Configure(EntityTypeBuilder<TranslationEntity> builder)
    {
        builder.ToTable("translation");
        
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Value).IsRequired().HasMaxLength(500);
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired();

        builder.HasOne(t => t.Key)
            .WithMany(k => k.Translations)
            .HasForeignKey(t => t.KeyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Language)
            .WithMany(l => l.Translations)
            .HasForeignKey(t => t.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(t => new { t.KeyId, t.LanguageId }).IsUnique();
        builder.HasData(Translations);
    }
}