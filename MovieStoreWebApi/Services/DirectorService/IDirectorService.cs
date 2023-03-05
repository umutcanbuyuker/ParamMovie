using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public interface IDirectorService
    {
        Director AddDirector(DirectorCreateDto directorDto);
        void DeleteDirector(int id);
        Director UpdateDirector(DirectorCreateDto directorDto, int id);
        DirectorListDto GetDirectorByDetails(int id);
        List<DirectorListDto> GetAll();
        bool DirectorExists(string directorName);
        //List<DirectorListDto> SearchMovieByName(string searchString);
    }
}
