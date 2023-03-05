using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Entities.DTOs;
using MovieStoreWebApi.Services.ActorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_actorService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_actorService.GetActorByDetails(id));
        }

        [HttpPost]
        public IActionResult AddDirector([FromBody] ActorDto director)
        {
            return Ok(_actorService.AddActor(director));
        }

        [HttpPut]
        public IActionResult UpdateDirector(int id, [FromBody] ActorDto actor)
        {
            return Ok(_actorService.UpdateActor(actor, id));
        }

        [HttpDelete]
        public IActionResult DeleteDirector(int id)
        {
            _actorService.DeleteActor(id);
            return Ok();
        }
    }
}
