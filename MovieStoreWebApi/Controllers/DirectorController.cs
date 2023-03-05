using Microsoft.AspNetCore.Mvc;
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
    public class DirectorController : Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_directorService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_directorService.GetDirectorByDetails(id));
        }

        [HttpPost]
        public IActionResult AddDirector([FromBody] DirectorCreateDto director)
        {
            return Ok(_directorService.AddDirector(director));
        }

        [HttpPut]
        public IActionResult UpdateDirector(int id, [FromBody] DirectorCreateDto director)
        {
            return Ok(_directorService.UpdateDirector(director, id));
        }

        [HttpDelete]
        public IActionResult DeleteDirector(int id)
        {
            _directorService.DeleteDirector(id);
            return Ok();
        }
    }
}
