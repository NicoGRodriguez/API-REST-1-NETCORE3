using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.Excepciones
{
    public class NegocioExcepcion : Exception
    {
        public NegocioExcepcion()
        {
              
        }
        public NegocioExcepcion(string message) : base(message)
        {

        }
    }
}
