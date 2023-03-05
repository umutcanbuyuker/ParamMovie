using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Common.Validations.ValidationRules;
using MovieStoreWebApi.Common.Validations.ValidationTool;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services.ActorService
{
    public class ActorService : IActorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool ActorExists(string actorName)
        {
            using (_context)
            {
                var actor = _context.Actors.SingleOrDefault(x => $"{x.FirstName.ToLower()} {x.LastName.ToLower()}" == actorName.ToLower());
                if (actor == null)
                {
                    return false;
                }
                return true;
            }
        }

        public Actor AddActor(ActorDto actorDto)
        {
            using (_context)
            {
                var actor = _context.Actors.SingleOrDefault(x => x.LastName.ToLower() == actorDto.LastName);

                if (actor != null)
                {
                    throw new InvalidOperationException("Oyuncu zaten mevcut.");
                }

                _context.Actors.Add(new Actor
                {
                    FirstName = actorDto.FirstName,
                    LastName = actorDto.LastName
                });

                ActorValidations validations = new ActorValidations();
                ValidationTool.Validate(validations, _context);
                _context.SaveChanges();

                return actor;
            }
        }

        public void DeleteActor(int id)
        {
            using (_context)
            {
                var actor = _context.Actors.SingleOrDefault(x => x.Id == id);

                if (actor == null)
                {
                    throw new InvalidOperationException("Oyuncu bulunamadı.");
                }

                _context.Actors.Remove(actor);
                _context.SaveChanges();
            }
        }

        public ActorListDto GetActorByDetails(int id)
        {
            using (_context)
            {
                var actor = _context.Actors.Include(x => x.MovieActor).ThenInclude(x => x.Movie).SingleOrDefault(x => x.Id == id);

                if (actor == null)
                {
                    throw new InvalidOperationException("Oyuncu bulunamadı.");
                }

                var actorDetails = _mapper.Map<ActorListDto>(actor);
                return actorDetails;
            }       
        }

        public List<ActorListDto> GetAll()
        {
            using (_context)
            {
                var actors = _context.Actors.Include(x => x.MovieActor).ThenInclude(x => x.Movie).OrderBy(x => x.Id);

                var actorLists = _mapper.Map<List<ActorListDto>>(actors);
                return actorLists;
            }
        }

        public ActorDto UpdateActor(ActorDto actorDto, int id)
        {
            using (_context)
            {
                var actor = _context.Actors.SingleOrDefault(x => x.Id == id);

                if (actor == null)
                {
                    throw new InvalidOperationException("Oyuncu bulunamadı");
                }

                actor.FirstName = actorDto.FirstName == default ? actor.FirstName : actorDto.LastName;
                actor.LastName = actorDto.LastName == default ? actor.LastName : actorDto.LastName;

                ActorValidations validations = new ActorValidations();
                ValidationTool.Validate(validations, _context);
                _context.SaveChanges();

                return actorDto;
            }
        }
    }
}
