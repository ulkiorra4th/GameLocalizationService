using GameLocalization.Persistence.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLocalization.Persistence.Postgres.Configurations;

internal sealed class KeyConfiguration : IEntityTypeConfiguration<KeyEntity>
{
    private static readonly List<KeyEntity> Keys = 
    [
        new()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "p_menu",
            ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Translations = new List<TranslationEntity?>()
        },
        new()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
            Name = "p_score_display_text",
            ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Translations = new List<TranslationEntity?>()
        },
        new()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
            Name = "p_settings",
            ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Translations = new List<TranslationEntity?>()
        },
        new()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
            Name = "p_inventory_item_name",
            ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Translations = new List<TranslationEntity?>()
        },
        new()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
            Name = "p_quit_confirmation_message",
            ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Translations = new List<TranslationEntity?>()
        },
    ];
    
    public void Configure(EntityTypeBuilder<KeyEntity> builder)
    {
        builder.ToTable("key");
        
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Name).IsRequired().HasMaxLength(200);
        builder.Property(k => k.CreatedAt).IsRequired();
        builder.Property(k => k.UpdatedAt).IsRequired();

        builder.HasOne(k => k.Project)
            .WithMany(p => p.Keys!)
            .HasForeignKey(k => k.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(Keys);
    }
}