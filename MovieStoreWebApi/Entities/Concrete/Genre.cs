using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.Concrete
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CustomerGenre> FavouriteGenres { get; set; }
    }
}
