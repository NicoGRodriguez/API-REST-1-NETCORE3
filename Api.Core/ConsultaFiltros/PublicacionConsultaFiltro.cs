using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.ConsultaFiltros
{
    public class PublicacionConsultaFiltro
    {
        public int? idUsuario { get; set; }
        public DateTime? fecha { get; set; }
        public string descripcion { get; set; }
    }
}
