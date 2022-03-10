using Api.Core.ConsultaFiltros;
using Api.infraestructura.Interfaces;
using System;

namespace Api.infraestructura.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostPaginationUri(PublicacionConsultaFiltro filtro, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
