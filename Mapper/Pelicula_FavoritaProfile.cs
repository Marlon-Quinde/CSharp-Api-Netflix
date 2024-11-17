using AutoMapper;
using Models.DTO.Pelicula_Favorita;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Pelicula_FavoritaProfile: Profile
    {
        public Pelicula_FavoritaProfile()
        {
            CreateMap<CrearPelicula_FavoritaDTO, Pelicula_Favorita>();
            CreateMap<Pelicula_Favorita, Pelicula_FavoritaDTO>();
        }
    }
}
