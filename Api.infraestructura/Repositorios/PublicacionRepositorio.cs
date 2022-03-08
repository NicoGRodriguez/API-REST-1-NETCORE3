using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.infraestructura.Repositorios
{
    public class PublicacionRepositorio : BaseRepositorio<Publicacion>, IPublicacionRepositorio
    {
        public PublicacionRepositorio(SocialApiContext context) : base(context) { }

        public async Task<IEnumerable<Publicacion>> GetPostsByUser(int idUsuario)
        {
            return await _entidades.Where(x => x.IdUsuario == idUsuario).ToListAsync();
        }
    }
}
