using Microsoft.EntityFrameworkCore;
using ReactMovieApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace ReactMovieApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
    }
}
