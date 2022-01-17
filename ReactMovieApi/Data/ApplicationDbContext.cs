using Microsoft.EntityFrameworkCore;
using ReactMovieApi.Models;
using ReactMovieApi.Models.LinkingTables;
using System.Diagnostics.CodeAnalysis;

namespace ReactMovieApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<GenreMovie>()
            .HasKey(x => new { x.GenreId, x.MovieId });

            modelBuilder.Entity<MovieMovieTheater>()
                .HasKey(x => new { x.MovieTheaterId, x.MovieId });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesActors> MovieActors { get; set; }
        public DbSet<GenreMovie> GenreMovies { get; set; }
        public DbSet<MovieMovieTheater> MovieMovieTheaters { get; set; }
    }
}
