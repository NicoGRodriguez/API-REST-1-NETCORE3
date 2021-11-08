using System;

namespace Api.Core.DTOs
{
    //dtos objetos planos para enviar informacion
    public class PublicacionDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }
}
