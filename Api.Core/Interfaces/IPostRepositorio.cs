using Api.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IPostRepositorio 
    {
        Task<IEnumerable<Publicacion>> GetPosts();
        Task<Publicacion> GetPost(int id);

    }
}
