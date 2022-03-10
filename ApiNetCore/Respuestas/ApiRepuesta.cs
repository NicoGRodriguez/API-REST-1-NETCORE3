using Api.Core.PersonalizadasEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Respuestas
{
    public class ApiRepuesta<T>
    {
        public ApiRepuesta(T data)
        {
            Data = data;
        }
        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
