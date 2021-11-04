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
        public async Task<bool> UpDatePost(Publicacion publi)
        {
            var currentPubli = await GetPost(publi.IdPublicacion);
            currentPubli.Fecha = publi.Fecha;
            currentPubli.Descripcion = publi.Descripcion;
            currentPubli.Comentario = publi.Comentario;
            currentPubli.Imagen = publi.Imagen;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
            //rows > 9 que tiene que haber al menos un registro
        }
        public async Task<bool> DeletePost(int Id)
        {
            var currentPubli = await GetPost(Id);
            _context.Publicacion.Remove(currentPubli);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
