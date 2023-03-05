using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public class TokenService
    {
        public IConfiguration Configuration { get; set; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(params string[] userEmailAndName)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["Token:SecretKey"]);
            var expireDate = DateTime.Now.AddMinutes(20);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmailAndName[0]),
                    new Claim(ClaimTypes.Name, userEmailAndName[1]),
                    new Claim(ClaimTypes.Surname, userEmailAndName[2]),
                    new Claim(ClaimTypes.NameIdentifier, userEmailAndName[3])
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Expires = expireDate
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var tokenModel = new Token
            {
                AccessToken = accessToken,
                ExpireDate = expireDate,
                RefreshToken = CreateRefreshToken()
            };

            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
