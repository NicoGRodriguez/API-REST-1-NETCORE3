using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.infraestructura.Datos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.infraestructura.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialApiContext _context;
        private readonly IRepository<Publicacion> _publicacionRepositorio;
        private readonly IRepository<Usuario> _usuarioRepositorio;
        private readonly IRepository<Comentario> _comentarioRepositorio;
        public UnitOfWork(SocialApiContext context)
        {
            _context = context;
        }
        public IRepository<Publicacion> PostRepositorio => _publicacionRepositorio ?? new BaseRepositorio<Publicacion>(_context);

        public IRepository<Usuario> UserRepositorio => _usuarioRepositorio ?? new BaseRepositorio<Usuario>(_context);

        public IRepository<Comentario> CommentRepositorio => _comentarioRepositorio ?? new BaseRepositorio<Comentario>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
