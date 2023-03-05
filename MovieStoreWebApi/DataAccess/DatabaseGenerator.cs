using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStoreWebApi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.DataAccess
{
    public class DatabaseGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                if (context.Genres.Any())
                {
                    return;
                }

                if (context.Actors.Any())
                {
                    return;
                }

                if (context.Directors.Any())
                {
                    return;
                }

                var genreList = new List<Genre>
                {
                    new Genre { Name = "Genre 1"},
                    new Genre { Name = "Genre 2"}
                };
                context.Genres.AddRange(genreList);
                context.SaveChanges();

                var movie1 = new Movie { Name = "Movie 1", GenreId = 1, Price = 10.0m, PublishYear = new DateTime(), DirectorId = 2 };
                var movie2 = new Movie { Name = "Movie 2", GenreId = 2, Price = 10.0m, PublishYear = new DateTime(), DirectorId = 3 };
                var movie3 = new Movie { Name = "Movie 3", GenreId = 2, Price = 10.0m, PublishYear = new DateTime(), DirectorId = 1 };
                var movie4 = new Movie { Name = "Movie 4", GenreId = 1, Price = 10.0m, PublishYear = new DateTime(), DirectorId = 4 };

                var movieList = new List<Movie>
                {
                    movie1, movie2, movie3, movie4
                };
                context.Movies.AddRange(movieList);

                var directorList = new List<Director>
                {
                    new Director { FirstName = "Carla", LastName = "Skinner", Movies = new List<Movie> { movie3 } },
                    new Director { FirstName = "Dean",  LastName = "Cleveland", Movies = new List<Movie> { movie1 } },
                    new Director { FirstName = "Janna", LastName = "Garza", Movies = new List<Movie> { movie2 } },
                    new Director { FirstName = "Stone", LastName = "Bean", Movies = new List<Movie> { movie4 } },
                };
                context.Directors.AddRange(directorList);
                context.SaveChanges();

                var actorList = new List<Actor>
                {
                    new Actor { FirstName = "Ashely", LastName = "Shaw", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 1 }, new MovieActor { MovieId = 3, ActorId = 1 }, new MovieActor { MovieId = 4, ActorId = 1 } } },
                    new Actor { FirstName = "Daryl", LastName = "Spence", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 2 }, new MovieActor { MovieId = 3, ActorId = 2 }, new MovieActor { MovieId = 4, ActorId = 2 } } },
                    new Actor { FirstName = "Drake", LastName = "Chaney", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 3 }, new MovieActor { MovieId = 3, ActorId = 3 }, new MovieActor { MovieId = 4, ActorId = 3 } } },
                    new Actor { FirstName = "Blossom", LastName = "Ortega", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 4 }, new MovieActor { MovieId = 3, ActorId = 4 }, new MovieActor { MovieId = 4, ActorId = 4 } } },
                    new Actor { FirstName = "Isaiah", LastName = "Anthony", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 5 }, new MovieActor { MovieId = 3, ActorId = 5 }, new MovieActor { MovieId = 4, ActorId = 5 } } },
                    new Actor { FirstName = "Indira", LastName = "Randolph", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 6 }, new MovieActor { MovieId = 3, ActorId = 6 }, new MovieActor { MovieId = 4, ActorId = 6 } } },
                    new Actor { FirstName = "Latifah", LastName = "Holland", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 7 }, new MovieActor { MovieId = 3, ActorId = 7 }, new MovieActor { MovieId = 4, ActorId = 7 } } },
                    new Actor { FirstName = "Randall", LastName = "Larson", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 8 }, new MovieActor { MovieId = 3, ActorId = 8 }, new MovieActor { MovieId = 4, ActorId = 8 } } },
                    new Actor { FirstName = "Rinah", LastName = "Harding", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 9 }, new MovieActor { MovieId = 3, ActorId = 9 }, new MovieActor { MovieId = 4, ActorId = 9 } } },
                    new Actor { FirstName = "Julian", LastName = "Stanton", MovieActor = new List<MovieActor> { new MovieActor { MovieId = 1, ActorId = 10 }, new MovieActor { MovieId = 3, ActorId = 10 }, new MovieActor { MovieId = 4, ActorId = 10 }, new MovieActor { MovieId = 2, ActorId = 10 } } }
                };

                
                
                context.Actors.AddRange(actorList);
                
                context.SaveChanges();
            }
        }
    }
}
