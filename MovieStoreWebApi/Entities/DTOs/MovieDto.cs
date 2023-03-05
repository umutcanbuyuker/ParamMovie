using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.DTOs
{
    public class MovieDto
    {
        public string Name { get; set; }
        public string GenreName { get; set; }
        public DateTime PublishYear { get; set; }
        public decimal Price { get; set; }
        public List<string> Actors { get; set; }
        public string DirectorName { get; set; }
    }
}
