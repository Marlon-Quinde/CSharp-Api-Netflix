using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class MovieContext: DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Pelicula_Favorita> Peliculas_Favorias { get; set; }
    }
}
