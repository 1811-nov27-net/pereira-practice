using Microsoft.EntityFrameworkCore;
using System;

namespace DataAcess
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        { }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<CastMember> CastMember { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
