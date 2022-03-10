namespace Api.Core.PersonalizadasEntidades
{
    public class Metadata
    {
        public int TotalItem { get; set; }
        public int CantidadItemPagina { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public bool IrPaginaPrevia { get; set; }
        public bool IrPaginaProxima { get; set; }
        public string PaginaPreviaUrl { get; set; }
        public string PaginaProximaUrl { get; set; }
    }
}
