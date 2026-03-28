using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Shared.Entities;

namespace TommyFlix.Api.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<UserRating> Ratings { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<UserFavorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Índice único: un user no puede poner 2 ratings a la misma peli
        modelBuilder.Entity<UserRating>()
            .HasIndex(r => new { r.UserId, r.MovieId })
            .IsUnique()
            .HasFilter("[MovieId] IS NOT NULL");

        modelBuilder.Entity<UserRating>()
            .HasIndex(r => new { r.UserId, r.SerieId })
            .IsUnique()
            .HasFilter("[SerieId] IS NOT NULL");

        // Índice único: un user no puede favoritear 2 veces la misma peli
        modelBuilder.Entity<UserFavorite>()
            .HasIndex(f => new { f.UserId, f.MovieId })
            .IsUnique()
            .HasFilter("[MovieId] IS NOT NULL");

        modelBuilder.Entity<UserFavorite>()
            .HasIndex(f => new { f.UserId, f.SerieId })
            .IsUnique()
            .HasFilter("[SerieId] IS NOT NULL");

        // Movie y Serie: TmdbId único
        modelBuilder.Entity<Movie>()
            .HasIndex(m => m.TmdbId).IsUnique();

        modelBuilder.Entity<Serie>()
            .HasIndex(s => s.TmdbId).IsUnique();
    }
}