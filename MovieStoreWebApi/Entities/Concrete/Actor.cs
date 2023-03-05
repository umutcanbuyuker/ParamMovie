using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.Concrete
{
    public class Actor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<MovieActor> MovieActor { get; set; }
    }
}
