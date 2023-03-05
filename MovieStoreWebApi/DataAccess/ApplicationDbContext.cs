using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerGenre> CustomerGenres { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(c => c.Id);
            modelBuilder.Entity<MovieActor>().HasOne(m => m.Movie).WithMany(m => m.MovieActor).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieActor>().HasOne(a => a.Actor).WithMany(a => a.MovieActor).HasForeignKey(a => a.ActorId);

            modelBuilder.Entity<CustomerGenre>().HasKey(x => x.Id);
            modelBuilder.Entity<CustomerGenre>().HasOne(x => x.Genre).WithMany(x => x.FavouriteGenres).HasForeignKey(x => x.GenreId);
            modelBuilder.Entity<CustomerGenre>().HasOne(x => x.Customer).WithMany(x => x.FavouriteGenre).HasForeignKey(x => x.CustomerId);
        }
    }
}
