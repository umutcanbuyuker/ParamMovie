using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.DTOs
{
    public class DirectorListDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Movies { get; set; }
    }
}
