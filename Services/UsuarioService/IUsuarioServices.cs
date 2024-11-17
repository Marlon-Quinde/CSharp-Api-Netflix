using Helpers;
using Models.DTO.Usuario;

namespace Services.UsuarioService
{
    public interface IUsuarioServices
    {
        Task<Response<string>> CreateUserService(CrearUsuarioDTO payload);
    }
}