﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Pelicula_Favorita
{
    public class CrearPelicula_FavoritaDTO
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Poster_path { get; set; }
        public string Overview { get; set; }
        public int UsuarioId { get; set; }
    }
}