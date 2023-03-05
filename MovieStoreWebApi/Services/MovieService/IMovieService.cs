using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public interface IMovieService
    {
        Movie AddMovie(MovieCreateDto movieDto);
        void DeleteMovie(int id);
        Movie UpdateMovie(MovieCreateDto movieDto, int id);
        MovieDto GetMovieByDetails(int id);
        List<MovieDto> GetAll();
        bool MovieExists(string movieName);
        List<MovieListDto> SearchMovieByName(string searchString);
    }
}
