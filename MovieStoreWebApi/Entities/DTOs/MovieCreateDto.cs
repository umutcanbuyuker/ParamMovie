using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.DTOs
{
    public class MovieCreateDto
    {
        public string Name { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishYear { get; set; }
        public decimal Price { get; set; }
        public int DirectorId { get; set; }
        public List<int> ActorIds { get; set; }
    }
}
