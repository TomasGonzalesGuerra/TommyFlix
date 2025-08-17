using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Shared.Entities;
using TommyFlix.Shared.Enums;

namespace TommyFlix.Api.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<CastMember> CastMembers { get; set; }
    public DbSet<UserRating> Ratings { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }
    public DbSet<MediaContent> MediaContents { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<MediaContentTag> MediaContentTags { get; set; }
    public DbSet<MediaContentGender> MediaContentGenders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Gender>().Property(g => g.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Tag>().Property(t => t.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Movie>().Property(m => m.Duration).IsRequired();
        
        modelBuilder.Entity<MediaContent>().HasDiscriminator<ContentType>("ContentType").HasValue<Movie>(ContentType.Movie).HasValue<Serie>(ContentType.Serie);
        modelBuilder.Entity<MediaContent>().HasIndex("ContentType");        
        
        modelBuilder.Entity<Serie>().HasMany(s => s.Seasons).WithOne(s => s.Serie).HasForeignKey(s => s.SerieId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Season>().HasMany(s => s.Episodes).WithOne(e => e.Season).HasForeignKey(e => e.SeasonId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Season>().HasIndex(s => new { s.SerieId, s.SeasonNumber }).IsUnique();
        modelBuilder.Entity<Episode>().HasIndex(e => new { e.SeasonId, e.EpisodeNumber }).IsUnique();

        modelBuilder.Entity<MediaContentGender>().HasKey(mcg => new { mcg.MediaContentId, mcg.GenderId });
        modelBuilder.Entity<MediaContentGender>().HasOne(mcg => mcg.MediaContent).WithMany(mc => mc.MediaContentGenders).HasForeignKey(mcg => mcg.MediaContentId);
        modelBuilder.Entity<MediaContentGender>().HasOne(mcg => mcg.Gender).WithMany(g => g.MediaContentGenders).HasForeignKey(mcg => mcg.GenderId);

        modelBuilder.Entity<MediaContentTag>().HasKey(mct => new { mct.MediaContentId, mct.TagId });
        modelBuilder.Entity<MediaContentTag>().HasOne(mct => mct.MediaContent).WithMany(mc => mc.MediaContentTags).HasForeignKey(mct => mct.MediaContentId);
        modelBuilder.Entity<MediaContentTag>().HasOne(mct => mct.Tag).WithMany(t => t.MediaContentTags).HasForeignKey(mct => mct.TagId);
    }
}