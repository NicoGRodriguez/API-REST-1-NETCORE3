using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.PersonalizadasEntidades
{
    public class ListaPagina<T> : List<T>
    {
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int CatidadItemPagina { get; set; }
        public int TotalItem { get; set; }

        public bool IrPaginaPrevia => PaginaActual > 1;
        public bool IrPaginaProxima => PaginaActual < TotalPaginas;
        public int? NumeroPaginaPrevia => IrPaginaPrevia ? PaginaActual - 1 : (int?)null;
        public int? NumeroPaginaProxima => IrPaginaProxima ? PaginaActual + 1 : (int?)null;
        public ListaPagina(List<T> items, int cantidad, int numeroPagina, int cantidadItemPagina)
        {
            TotalItem = cantidad;
            CatidadItemPagina = cantidadItemPagina;
            PaginaActual = numeroPagina;
            TotalItem = (int)Math.Ceiling(cantidad / (double)cantidadItemPagina);

            AddRange(items);
        }
        public static ListaPagina<T> Creacion(IEnumerable<T> fuente, int numeroPagina, int cantidadItemPagina)
        {
            var cantidad = fuente.Count();
            var items = fuente.Skip((numeroPagina - 1) * cantidadItemPagina).Take(cantidadItemPagina).ToList();
            return new ListaPagina<T>(items, cantidad, numeroPagina, cantidadItemPagina);
        }
    }
}
