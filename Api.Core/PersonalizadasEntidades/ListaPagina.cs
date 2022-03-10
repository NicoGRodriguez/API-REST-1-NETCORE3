using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.PersonalizadasEntidades
{
    public class ListaPagina<T> : List<T>
    {
        public int PaginaActual { get; set; } //CurrenPage
        public int TotalPaginas { get; set; } //TotalPages
        public int ItemsPaginaTrae { get; set; } //ItemsPaginaTrae=PageSize
        public int TotalItem { get; set; } //Totalcount

        public bool IrPaginaPrevia => PaginaActual > 1; 
        public bool IrPaginaProxima => PaginaActual < TotalPaginas; 
        public int? NumeroPaginaPrevia => IrPaginaPrevia ? PaginaActual - 1 : (int?)null;
        public int? NumeroPaginaProxima => IrPaginaProxima ? PaginaActual + 1 : (int?)null;
        public ListaPagina(List<T> items, int cantidad, int numeroPagina, int itemsPaginaTrae)
        {
            TotalItem = cantidad;
            ItemsPaginaTrae = itemsPaginaTrae;
            PaginaActual = numeroPagina;
            TotalPaginas = (int)Math.Ceiling(cantidad / (double)itemsPaginaTrae);

            AddRange(items);
        }
        public static ListaPagina<T> Creacion(IEnumerable<T> fuente, int numeroPagina, int ItemsPaginaTrae)
        {
            var cantidad = fuente.Count();
            var items = fuente.Skip((numeroPagina - 1) * ItemsPaginaTrae).Take(ItemsPaginaTrae).ToList();
            return new ListaPagina<T>(items, cantidad, numeroPagina, ItemsPaginaTrae);
        }
    }
}
