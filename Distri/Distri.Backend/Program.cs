using Distri.Backend.Data;
using Distri.Backend.Repositories.Implementations;
using Distri.Backend.Repositories.Interfaces;
using Distri.Backend.UnitsOfWork.Implementations;
using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
builder.Services.AddTransient<SeedDb>();
//INYECCIONES GENERICAS
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//INYECCIONES DE PAISES
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();
//INYECCIONES DE PROVINCIAS
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();
//INYECCIONES DE CIUDADES
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();
//INYECCIONES DE CATEGORIAS
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesUnitOfWork, CategoriesUnitOfWork>();
//INYECCIONES DE USUARIOS
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
var app = builder.Build();

SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
