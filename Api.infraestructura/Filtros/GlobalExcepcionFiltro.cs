using Api.Core.Excepciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Api.infraestructura.Filtros
{
    public class GlobalExcepcionFiltro : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof (NegocioExcepcion))
            {
                var excepcion = (NegocioExcepcion)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Title = "BadRequest",
                    Detail  = excepcion.Message
                };
                var json = new
                {
                    errors = new[] {validation}
                };
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
