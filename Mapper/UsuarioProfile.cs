using AutoMapper;
using Models.DTO.Usuario;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class UsuarioProfile: Profile
    {
        public UsuarioProfile()
        {
            
            CreateMap<Usuario, CrearUsuarioDTO>();
            CreateMap<CrearUsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();

        }
    }
}
