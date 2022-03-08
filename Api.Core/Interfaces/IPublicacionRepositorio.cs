using Api.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IPublicacionRepositorio : IRepositorio<Publicacion>
    {
        Task<IEnumerable<Publicacion>> GetPostsByUser(int idUsuario);  
    }
}
