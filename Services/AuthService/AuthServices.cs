using DataContext;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Auth;
using Models.Entities;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.DTO.Usuario;

namespace Services.AuthService
{
    public class AuthServices : IAuthServices
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;
        public AuthServices(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }

        public async Task<Response<UsuarioDTO>> SignInServices(SignInDTO payload)
        {
            try
            {
                Usuario? existeUsuario = await _movieContext.Usuarios.FirstOrDefaultAsync(x => x.Email == payload.Email);

                if (existeUsuario == null)
                {
                    return new()
                    {
                        Code = HttpStatusCode.NotFound,
                        Message = $"No existe usuario registrado con ese correo.",
                        Data = null,
                    };
                }

                if (!Encrypter.ValidatePassword(payload.Password, existeUsuario.Password))
                {
                    return new()
                    {
                        Code = HttpStatusCode.Forbidden,
                        Message = $"El correo o la contraseña esta mal.",
                        Data = null
                    };
                }

                UsuarioDTO usuario = _mapper.Map<UsuarioDTO>(existeUsuario);

                return new()
                {
                    Code = HttpStatusCode.OK,
                    Message = $"Inicion de sesion exitoso.",
                    Data = usuario
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
