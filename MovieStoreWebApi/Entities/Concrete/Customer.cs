using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.Concrete
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<CustomerGenre> FavouriteGenre { get; set; }
        //public ICollection<CustomerMovie> BoughtMovies { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
