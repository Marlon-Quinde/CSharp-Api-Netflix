using DataContext;
using Mapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Pelicula_Favorita;
using Services.AuthService;
using Services.PeliculaFavoritaService;
using Services.UsuarioService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(cors => cors.AddPolicy("AllowWebApp", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Context
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Inyeccion de dependencias
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IPeliculasFavoritasServices, PeliculasFavoritasServices>();

// ? AutoMapper
builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(CrearPelicula_FavoritaDTO));

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

app.UseCors("AllowWebApp");

app.Run();
