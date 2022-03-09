using Api.Core.ConsultaFiltros;
using Api.Core.Entidades;
using Api.Core.PersonalizadasEntidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IPublicacionServicio
    {
        Task<Publicacion> GetPost(int id);
        ListaPagina<Publicacion> GetPosts(PublicacionConsultaFiltro filtros);        
        Task InsertPost(Publicacion post);
        Task<bool> UpDatePost(Publicacion publi);
        Task<bool> DeletePost(int Id);
    }
}