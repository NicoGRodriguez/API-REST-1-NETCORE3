using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.infraestructura.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SocialApiContext _context;
        public UsuarioRepositorio(SocialApiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var posts = await _context.Usuario.ToListAsync();
            return posts;

        }
        public async Task<Usuario> GetUsuario(int id)
        {
            var posts = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
            return posts;

        }
    }
}
