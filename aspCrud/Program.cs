global using aspCrud.Models;
global using aspCrud.Repositories.RoleRepository;
global using aspCrud.Services.RoleService;
global using aspCrud.Repositories.UserRepository;
global using aspCrud.Services.UserService;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;

// Add services to the container.

service.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();
service.AddDbContext<BEDBContext>();
service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Service
service.AddTransient<IRoleService, RoleService>();
service.AddTransient<IUserService, UserService>();

// Repository
service.AddTransient<IRoleRepository, RoleRepository>();
service.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();