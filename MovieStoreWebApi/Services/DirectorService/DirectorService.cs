using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Common.Validations.ValidationRules.Director;
using MovieStoreWebApi.Common.Validations.ValidationTool;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DirectorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Director AddDirector(DirectorCreateDto directorDto)
        {
            //var director = _mapper.Map<Director>(movieDto);
            var director = new Director
            {
                FirstName = directorDto.FirstName,
                LastName = directorDto.LastName
            };
            //DirectorValidations validations = new DirectorValidations();
            //ValidationTool.Validate(validations, _context);
            _context.Directors.Add(director);
            _context.SaveChanges();

            return director;
        }

        public void DeleteDirector(int id)
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == id);

            if (director == null)
            {
                throw new InvalidOperationException("Silmeye çalışılan tönetmen bulunamadı.");
            }

            _context.Directors.Remove(director);
            _context.SaveChanges();
        }

        public List<DirectorListDto> GetAll()
        {
            var directorList = _context.Directors.AsNoTracking().Include(x => x.Movies).OrderBy(x => x.Id).ToList();

            var directorListDto = new List<DirectorListDto>();

            foreach (var item in directorList)
            {
                directorListDto.Add(new DirectorListDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Movies = item.Movies.Where(x => x.DirectorId == item.Id).Select(x => $"{ x.Name }").ToList()
                });
            }
            return directorListDto; 
        }

        public DirectorListDto GetDirectorByDetails(int id)
        {
            var director = _context.Directors.AsNoTracking().Include(x => x.Movies).SingleOrDefault(x => x.Id == id);

            var directorDto = new DirectorListDto
            {
                FirstName = director.FirstName,
                LastName = director.LastName,
                Movies = director.Movies.Where(x => x.DirectorId == id).Select(x => $"{x.Director.FirstName} {x.Director.LastName}").ToList()
            };

            return directorDto;
        }

        public bool DirectorExists(string directorName)
        {
            var director = _context.Directors.Any(x => $"{ x.FirstName.ToLower() } { x.LastName.ToLower() }" == directorName.ToLower());

            if (!director)
            {
                throw new InvalidOperationException("Yönetmen bulunamadı.");
            }

            return director;
        }

        public Director UpdateDirector(DirectorCreateDto directorDto, int id)
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == id);

            if (director == null)
            {
                throw new InvalidOperationException("Yönetmen bulunamadı.");
            }
            
            director.FirstName = directorDto.FirstName == default ? director.FirstName : directorDto.FirstName;
            director.LastName = directorDto.LastName == default ? director.LastName : directorDto.LastName;
            _context.SaveChanges();

            return director;
        }
    }
}

