using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataContext;
using Models.Entities;
using Services.PeliculaFavoritaService;
using Models.DTO.Pelicula_Favorita;
using Helpers;
using System.Net;

namespace ApiNetflixApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pelicula_FavoritaController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IPeliculasFavoritasServices _peliculasFavoritasServices;

        public Pelicula_FavoritaController(MovieContext context, IPeliculasFavoritasServices peliculasFavoritasServices)
        {
            _context = context;
            _peliculasFavoritasServices = peliculasFavoritasServices;
        }

        // GET: api/Pelicula_Favorita
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Pelicula_Favorita>>> GetPeliculas_Favorias(int id)
        {
            try
            {
                var response = await _peliculasFavoritasServices.ListarFavoritasServices(id);
                return StatusCode((int)response.Code, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    Code = System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        // PUT: api/Pelicula_Favorita/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelicula_Favorita(int id, Pelicula_Favorita pelicula_Favorita)
        {
            if (id != pelicula_Favorita.Id)
            {
                return BadRequest();
            }

            _context.Entry(pelicula_Favorita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Pelicula_FavoritaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pelicula_Favorita
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPelicula_Favorita(CrearPelicula_FavoritaDTO payload)
        {
            try
            {
                var response = await _peliculasFavoritasServices.AgregarPeliFavoritaService(payload);
                return StatusCode((int)response.Code, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        // DELETE: api/Pelicula_Favorita/5
        [HttpDelete("{id}/{idMovie}")]
        public async Task<IActionResult> DeletePelicula_Favorita(int id, int idMovie)
        {
            try
            {
                var response = await _peliculasFavoritasServices.QuitarFavoritosServices(id, idMovie);
                return StatusCode((int)response.Code,response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        private bool Pelicula_FavoritaExists(int id)
        {
            return _context.Peliculas_Favorias.Any(e => e.Id == id);
        }
    }
}
