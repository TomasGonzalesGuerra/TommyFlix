using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Shared.Entities;

namespace TommyFlix.Api.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<CastMember> CastMembers { get; set; }
    public DbSet<UserRating> Ratings { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Gender>().Property(g => g.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Tag>().Property(t => t.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Movie>().Property(m => m.Duration).IsRequired();

        modelBuilder.Entity<Serie>().HasMany(s => s.Seasons).WithOne(s => s.Serie).HasForeignKey(s => s.SerieId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Season>().HasMany(s => s.Episodes).WithOne(e => e.Season).HasForeignKey(e => e.SeasonId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Season>().HasIndex(s => new { s.SerieId, s.SeasonNumber }).IsUnique();
        modelBuilder.Entity<Episode>().HasIndex(e => new { e.SeasonId, e.EpisodeNumber }).IsUnique();
    }
}