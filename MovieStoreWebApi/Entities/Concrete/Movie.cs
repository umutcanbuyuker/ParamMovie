using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.Concrete
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime PublishYear { get; set; }
        public decimal Price { get; set; }
        public ICollection<MovieActor> MovieActor { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
