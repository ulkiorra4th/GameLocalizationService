using GameLocalization.Persistence.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLocalization.Persistence.Postgres.Connection;

internal sealed class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<KeyEntity> Keys { get; set; }
    public DbSet<TranslationEntity> Translations { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<LanguageInTableEntity> LanguagesInTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
}