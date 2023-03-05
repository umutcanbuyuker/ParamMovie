using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieStoreWebApi.Common.Middlewares;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Services;
using MovieStoreWebApi.Services.ActorService;
using MovieStoreWebApi.Services.LogService;
using MovieStoreWebApi.Services.OrderService;
using System;
using System.Reflection;
using System.Text;

namespace MovieStoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                string issuer = Configuration["Token:Issuer"];
                string audience = Configuration["Token:Audience"];
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ApplicationDB;Integrated Security=True;"));
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCustomException();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
