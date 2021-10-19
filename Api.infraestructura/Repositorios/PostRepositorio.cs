using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.infraestructura.Repositorios
{
    public class PostRepositorio : IPostRepositorio
    {
        private readonly SocialApiContext _context;
        public PostRepositorio(SocialApiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Publicacion>> GetPosts()
        {
            var posts = await _context.Publicacion.ToListAsync();

            return posts;

        }
    }
}
