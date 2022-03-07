using Api.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IPublicacionRepositorio
    {
        Task<IEnumerable<Publicacion>> GetPosts();
        Task<Publicacion> GetPost(int id);
        Task InsertPost(Publicacion post);
        Task<bool> UpDatePost(Publicacion publi);
        Task<bool> DeletePost(int Id);
    }
}
