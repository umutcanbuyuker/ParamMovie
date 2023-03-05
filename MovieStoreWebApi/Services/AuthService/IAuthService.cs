using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public interface IAuthService
    {
        public Token Auth(CustomerAuthDto customerAuthDto);
        public Token RefreshToken(string refreshToken);
    }
}
