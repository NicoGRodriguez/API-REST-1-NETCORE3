using Api.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Publicacion> PostRepositorio { get; }
        IRepository<Usuario> UserRepositorio { get; }
        IRepository<Comentario> CommentRepositorio { get; }

        void SaveChanges();
        Task SaveChangesAsync();

        }
}
