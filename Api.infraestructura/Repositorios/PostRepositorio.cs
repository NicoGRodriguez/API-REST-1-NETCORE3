using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public async Task<Publicacion> GetPost(int id)
        {
            var posts = await _context.Publicacion.FirstOrDefaultAsync(x=> x.IdPublicacion == id);

            return posts;

        }
        public async Task InsertPost(Publicacion post)
        {
            _context.Publicacion.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
