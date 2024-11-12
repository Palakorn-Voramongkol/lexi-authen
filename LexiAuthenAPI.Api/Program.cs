// LexiAuthenAPI.Api/Program.cs
using Microsoft.EntityFrameworkCore;
using LexiAuthenAPI.Infrastructure.Data;
using LexiAuthenAPI.Api.Services;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LexiAuthenAPI.Api.Configuration;
using FluentValidation.AspNetCore;
using Serilog;
using LexiAuthenAPI.Api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 0. Add services to the container.
builder.Services.AddControllers();

// 1. Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// 2. Register ApplicationDbContext with SQL Server provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IUipermissionRepository, UipermissionRepository>();
builder.Services.AddScoped<IUiitemRepository, UiitemRepository>();

// 4. Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>(); // Register PasswordHasherService
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUipermissionService, UipermissionService>();
builder.Services.AddScoped<IUiitemService, UiitemService>();

// 5. Register AutoMapper with the MappingProfile configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 6. Register Controllers with FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>(); // register validators

builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
builder.Services.AddFluentValidationClientsideAdapters(); // for client side
// 7. Register Swagger
builder.Services.AddEndpointsApiExplorer();
// Add Swagger services with XML comments
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LexiAuthenAPI", Version = "v1.0" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// 8. Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("https://yourdomain.com") // Replace with your frontend domain
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// 9. Configure JWT Authentication
// Configure JwtSettings from the configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Check if JwtSettings or any of its required properties are null or empty and throw an exception
if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Secret) || string.IsNullOrEmpty(jwtSettings.Issuer) || string.IsNullOrEmpty(jwtSettings.Audience))
{
    throw new InvalidOperationException("JwtSettings are not configured correctly. Please ensure 'Secret', 'Issuer', and 'Audience' are set in the configuration.");
}

// Get the key for token generation
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Validate JwtSettings before applying them
    var issuer = builder.Configuration["JwtSettings:Issuer"];
    var audience = builder.Configuration["JwtSettings:Audience"];
    var secret = builder.Configuration["JwtSettings:Secret"];
    if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secret))
    {
        throw new InvalidOperationException("JwtSettings are not configured correctly. Ensure 'Issuer', 'Audience', and 'Secret' are present in the configuration.");
    }
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
    };
});
// Add authorization service
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
