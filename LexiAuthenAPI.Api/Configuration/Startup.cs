// LexiAuthenAPI.Api/Configuration/Startup.cs
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LexiAuthenAPI.Api.Configuration;
using LexiAuthenAPI.Api.Mappers;
using LexiAuthenAPI.Api.Services;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using LexiAuthenAPI.Infrastructure.Repositories;
using System.Text;

namespace LexiAuthenAPI.Api.Configuration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure JwtSettings
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            // Configure DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Configure Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateLifetime = true,
                    ClockSkew = System.TimeSpan.Zero // Eliminate clock skew
                };
            });

            // Add controllers
            services.AddControllers();

            // Add Swagger for API documentation
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Use developer exception page
            app.UseDeveloperExceptionPage();

            // Use Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LexiAuthenAPI V1");
            });

            app.UseRouting();

            // Use Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
