using Helpers;
using Models.DTO.Pelicula_Favorita;

namespace Services.PeliculaFavoritaService
{
    public interface IPeliculasFavoritasServices
    {
        Task<Response<string>> AgregarPeliFavoritaService(CrearPelicula_FavoritaDTO payload);
        Task<Response<List<Pelicula_FavoritaDTO>>> ListarFavoritasServices(int usuarioId);
        Task<Response<string>> QuitarFavoritosServices(int id, int movieId);
    }
}