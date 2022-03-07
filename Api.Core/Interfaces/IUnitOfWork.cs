using Api.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositorio<Publicacion> PostRepositorio { get; }
        IRepositorio<Usuario> UserRepositorio { get; }
        IRepositorio<Comentario> CommentRepositorio { get; }

        void SaveChanges();
        Task SaveChangesAsync();

        }
}
