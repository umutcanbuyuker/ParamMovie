using Microsoft.Extensions.Configuration;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Linq;

namespace MovieStoreWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration { get; set; }

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public Token Auth(CustomerAuthDto customerAuthDto)
        {
            var user = _context.Customers.SingleOrDefault(x => x.Email == customerAuthDto.Email && x.Password == customerAuthDto.Password);

            if (user == null)
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            }

            TokenService tokenService = new TokenService(Configuration);
            var token = tokenService.CreateAccessToken(user.Email, user.FirstName, user.LastName, user.Id.ToString());

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = DateTime.Now.AddMinutes(5);
            _context.SaveChanges();

            return new Token
            {
                AccessToken = token.AccessToken,
                ExpireDate = token.ExpireDate,
                RefreshToken = token.RefreshToken
            };
        }

        public Token RefreshToken(string refreshToken)
        {
            var user = _context.Customers.SingleOrDefault(x => x.RefreshToken == refreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if (user == null)
            {
                throw new InvalidOperationException("Token geçersiz.");
            }

            var tokenService = new TokenService(Configuration);
            var token = tokenService.CreateAccessToken(user.Email, user.FirstName, user.LastName);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = DateTime.Now.AddMinutes(1);
            _context.SaveChanges();

            return token;
        }
    }
}
