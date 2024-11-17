using Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DataContext;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Usuario;

namespace Services.UsuarioService
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;
        public UsuarioServices(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }
        public async Task<Response<string>> CreateUserService(CrearUsuarioDTO payload)
        {
            try
            {
                Usuario? existeUsuario = await _movieContext.Usuarios.FirstOrDefaultAsync(x => x.Email.ToUpper().Contains(payload.Email.ToUpper()));

                if (existeUsuario != null)
                {
                    return new()
                    {
                        Code = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = $"Ya existe un usuario registrado con ese correo"
                    };
                }
                payload.Password = Encrypter.HashPassword(payload.Password);

                Usuario usuario = _mapper.Map<Usuario>(payload);
                var nuevoUsuario = await _movieContext.Usuarios.AddAsync(usuario);
                await _movieContext.SaveChangesAsync();

                
                return new()
                {
                    Code = HttpStatusCode.Created,
                    Message = $"Usuario {payload.Nickname} creado con exito.",
                    Data = null,
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
