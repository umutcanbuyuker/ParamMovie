using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services.ActorService
{
    public interface IActorService
    {
        Actor AddActor(ActorDto actorDto);
        void DeleteActor(int id);
        ActorDto UpdateActor(ActorDto actorDto, int id);
        ActorListDto GetActorByDetails(int id);
        List<ActorListDto> GetAll();
        bool ActorExists(string actorName);
    }
}
