using DataContext;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Pelicula_Favorita;
using Models.Entities;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services.PeliculaFavoritaService
{
    public class PeliculasFavoritasServices : IPeliculasFavoritasServices
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;
        public PeliculasFavoritasServices(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }

        public async Task<Response<List<Pelicula_FavoritaDTO>>> ListarFavoritasServices(int usuarioId)
        {
            try
            {
                List<Pelicula_Favorita> listadoFavoritas = await _movieContext.Peliculas_Favorias.Where(x => x.UsuarioId == usuarioId).ToListAsync();

                List<Pelicula_FavoritaDTO> listado = _mapper.Map<List<Pelicula_FavoritaDTO>>(listadoFavoritas);

                return new()
                {
                    Code = HttpStatusCode.OK,
                    Data = listado,
                    Message = "Transsación éxitosa"
                };
            }
            catch (Exception ex)
            {

                return new()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<Response<string>> AgregarPeliFavoritaService(CrearPelicula_FavoritaDTO payload)
        {
            try
            {
                Pelicula_Favorita? existePeliculaAgregada = await _movieContext.Peliculas_Favorias.FirstOrDefaultAsync(x => x.MovieId == payload.MovieId && x.UsuarioId == payload.UsuarioId);

                if (existePeliculaAgregada != null)
                {
                    return new()
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = $"Ya se encuentra agregada a favoritos",
                        Data = null
                    };
                }

                Pelicula_Favorita pelicula = _mapper.Map<Pelicula_Favorita>(payload);
                var nuevPelicula = await _movieContext.Peliculas_Favorias.AddAsync(pelicula);

                await _movieContext.SaveChangesAsync();

                return new()
                {
                    Code = HttpStatusCode.Created,
                    Message = $"Peicula {payload.Title} agregada a favoritos",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<Response<string>> QuitarFavoritosServices(int id, int movieId)
        {
            try
            {
                var pelicula_Favorita = await _movieContext.Peliculas_Favorias.FirstOrDefaultAsync( x => x.MovieId == movieId.ToString() && x.UsuarioId == id);
                if (pelicula_Favorita == null)
                {
                    return new()
                    {
                        Code = HttpStatusCode.NotFound,
                        Message = "No se encontro esa pelicula",
                        Data = null,
                    };
                }

                _movieContext.Peliculas_Favorias.Remove(pelicula_Favorita);
                await _movieContext.SaveChangesAsync();

                return new()
                {
                    Code = HttpStatusCode.OK,
                    Message = $"Pelicula {pelicula_Favorita.Title} eliminada de Favoritos",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                };

            }
        }
    }
}
