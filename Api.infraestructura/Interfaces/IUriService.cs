using Api.Core.ConsultaFiltros;
using System;

namespace Api.infraestructura.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PublicacionConsultaFiltro filtro, string actionUrl);
    }
}