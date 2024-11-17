using Helpers;
using Models.DTO.Auth;
using Models.DTO.Usuario;

namespace Services.AuthService
{
    public interface IAuthServices
    {
        Task<Response<UsuarioDTO>> SignInServices(SignInDTO payload);
    }
}