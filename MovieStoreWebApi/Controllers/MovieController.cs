using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.Entities.DTOs;
using MovieStoreWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = _movieService.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_movieService.GetMovieByDetails(id));
        }

        [HttpGet("{searchStr}")]
        public IActionResult SearchMovieByName(string searchStr)
        {
            var movies = _movieService.SearchMovieByName(searchStr);
            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MovieCreateDto movieDto)
        {
            _movieService.AddMovie(movieDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody] MovieCreateDto movieDto)
        {
            _movieService.UpdateMovie(movieDto, id);
            return Ok();
        }

        [HttpDelete("{id:int}")]
       public IActionResult Delete(int id)
        {
            _movieService.DeleteMovie(id);
            return Ok();
        }

       
    }
}
