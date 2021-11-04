using Api.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> GetUsuario(int id);
        Task<IEnumerable<Usuario>> GetUsuarios();
    }
}