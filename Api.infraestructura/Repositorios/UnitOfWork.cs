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
        private readonly IPublicacionRepositorio _publicacionRepositorio;
        private readonly IRepositorio<Usuario> _usuarioRepositorio;
        private readonly IRepositorio<Comentario> _comentarioRepositorio;
        public UnitOfWork(SocialApiContext context)
        {
            _context = context;
        }
        public IPublicacionRepositorio PostRepositorio => _publicacionRepositorio ?? new PublicacionRepositorio(_context);

        public IRepositorio<Usuario> UserRepositorio => _usuarioRepositorio ?? new BaseRepositorio<Usuario>(_context);

        public IRepositorio<Comentario> CommentRepositorio => _comentarioRepositorio ?? new BaseRepositorio<Comentario>(_context);

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
