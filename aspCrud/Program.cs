global using aspCrud.Models;
global using aspCrud.Repositories.RoleRepository;
global using aspCrud.Services.RoleService;
global using aspCrud.Repositories.UserRepository;
global using aspCrud.Services.UserService;
global using aspCrud.Models.DTO;
using System.Net;
using System.Text;
using aspCrud.Services.PasswordService;
using aspCrud.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.

// Config option
services.Configure<TokenOptions>(options => configuration.GetSection("TokenOptions").Bind(options));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo{Title = "Crud API asp", Version = "v1"});
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

services.AddDbContext<BEDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("connection")), ServiceLifetime.Transient);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Service
services.AddTransient<IRoleService, RoleService>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<IPasswordService, PasswordService>();
services.AddTransient<ITokenService, TokenService>();

// Repository
services.AddTransient<IRoleRepository, RoleRepository>();
services.AddTransient<IUserRepository, UserRepository>();

// JWT
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = configuration["TokenOptions:Issuer"],
        ValidAudience = configuration["TokenOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();