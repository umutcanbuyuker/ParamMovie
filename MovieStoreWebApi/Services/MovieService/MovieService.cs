using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Common.Validations.ValidationRules;
using MovieStoreWebApi.Common.Validations.ValidationTool;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieStoreWebApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Movie AddMovie(MovieCreateDto movieDto)
        {
            using (_context)
            {
                if (MovieExists(movieDto.Name))
                {
                    throw new InvalidOperationException("");
                }

                var movie = _mapper.Map<Movie>(movieDto);
                
                MovieValidatons validationRules = new MovieValidatons();
                ValidationTool.Validate(validationRules, movieDto);

                _context.Movies.Add(movie);
                _context.SaveChanges();

                foreach (var item in movieDto.ActorIds)
                {
                    var movieActors = new MovieActor { ActorId = item, MovieId = movie.Id };
                    _context.MovieActors.Add(movieActors);
                    _context.SaveChanges();
                }

                return movie;
            }
        }

        public void DeleteMovie(int id)
        {
            using (_context)
            {
                var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
                var movieActors = _context.MovieActors.Where(x => x.MovieId == id);
                if (movie == null)
                {
                    throw new InvalidOperationException("Film bulunamadı.");
                }

                if (movieActors == null)
                {
                    throw new InvalidOperationException("Aktörler bulunamadı.");
                }

                _context.Movies.Remove(movie);

                foreach (var item in movieActors)
                {
                    _context.MovieActors.Remove(item);
                }
                _context.SaveChanges();
            }
        }

        public MovieDto GetMovieByDetails(int id)
        {
            using (_context)
            {
                var movie = _context.Movies
                    .Include(x => x.Genre)
                    .Include(x => x.Director)
                    .Include(x => x.MovieActor)
                    .ThenInclude(x => x.Actor)
                    .Where(x => x.Id == id).SingleOrDefault();

                if (movie == null)
                {
                    throw new InvalidOperationException("");
                }

                var movieDto = new MovieDto
                {
                    Name = movie.Name,
                    GenreName = movie.Genre.Name,
                    Price = movie.Price,
                    PublishYear = movie.PublishYear,
                    Actors = movie.MovieActor.Where(x => x.MovieId == id).Select(x => $"{x.Actor.FirstName} {x.Actor.FirstName}").ToList(),
                    DirectorName = $"{movie.Director.FirstName} {movie.Director.LastName}"
                };
                return movieDto;
            }
        }

        public List<MovieDto> GetAll()
        {
            using (_context)
            {
                var list = _context.Movies
                    .Include(x => x.Genre)
                    .Include(x => x.Director)
                    .Include(x => x.MovieActor)
                    .ThenInclude(x => x.Actor)
                    .OrderBy(x => x.Id).ToList();
                var movieList = new List<MovieDto>();
                foreach (var item in list)
                {
                    var movie = new MovieDto
                    {
                        Name = item.Name,
                        GenreName = item.Genre.Name,
                        Price = item.Price,
                        PublishYear = item.PublishYear,
                        Actors = item.MovieActor.Where(x => x.MovieId == item.Id).Select(x => $"{x.Actor.FirstName} {x.Actor.LastName}").ToList(),
                        DirectorName = $"{item.Director.FirstName} {item.Director.LastName}"
                    };
                    movieList.Add(movie);
                }
                return movieList;
            }
        }

        public List<MovieListDto> SearchMovieByName(string searchString)
        {
            using (_context)
            {
                var movie = _context.Movies
                    .Include(x => x.Genre)
                    .Include(x => x.Director)
                    .Include(x => x.MovieActor)
                    .ThenInclude(x => x.Actor)
                    .Where(x => x.Name.Contains(searchString));

                if (movie == null)
                {
                    throw new InvalidOperationException("Aranan kayıt bulunamadı.");
                }

                var movieList = new List<MovieListDto>();

                foreach (var item in movie)
                {
                    movieList.Add(new MovieListDto
                    {
                        Name = item.Name,
                        GenreName = item.Genre.Name,
                        Price = item.Price,
                        PublishYear = item.PublishYear,
                        DirectorName = $"{item.Director.FirstName} {item.Director.LastName}",
                        Actors = item.MovieActor.Where(x => x.MovieId == item.Id).Select(x => $"{x.Actor.FirstName} {x.Actor.LastName}").ToList()
                    });
                }
                return movieList;
            }
        }

        public bool MovieExists(string movieName)
        {
            return _context.Movies.Any(x => x.Name == movieName);
        }

        public Movie UpdateMovie(MovieCreateDto movieDto, int id)
        {
            using (_context)
            {
                var movies = _context.Movies
                .Include(x => x.Genre)
                .Include(x => x.MovieActor)
                .SingleOrDefault(x => x.Id == id);

                if (movies == null)
                {
                    throw new InvalidOperationException("Film bulunamadı.");
                }

                movies.Name = movieDto.Name == default ? movies.Name : movieDto.Name;
                movies.GenreId = movieDto.GenreId == default ? movies.GenreId : movieDto.GenreId;
                movies.Price = movieDto.Price == default ? movies.Price : movieDto.Price;
                movies.PublishYear = movieDto.PublishYear == default ? movies.PublishYear : movieDto.PublishYear;
                movies.DirectorId = movieDto.DirectorId == default ? movies.DirectorId : movieDto.DirectorId;

                var movieActors = movies.MovieActor.ToList();
                int i = 0;
                foreach (var item in movieActors)
                {
                    if (i > movieActors.Count)
                    {
                        break;
                    }

                    if (item.ActorId != movieDto.ActorIds[i])
                    {
                        item.ActorId = movieDto.ActorIds[i] == default ? item.ActorId : movieDto.ActorIds[i];
                        _context.SaveChanges();
                    }
                    i++;
                }

                MovieValidatons validations = new MovieValidatons();
                ValidationTool.Validate(validations, _context);

                _context.SaveChanges();
                return movies;
            }
        }
    }
}

