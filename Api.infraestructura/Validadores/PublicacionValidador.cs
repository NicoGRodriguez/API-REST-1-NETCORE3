using Api.Core.DTOs;
using FluentValidation;
using System;

namespace Api.infraestructura.Validadores
{
    public class PublicacionValidador : AbstractValidator<PublicacionDTO>
    {
        public PublicacionValidador()
        {
            RuleFor(Publicacion => Publicacion.Descripcion)
                .NotNull()
                .Length(10, 500);
            RuleFor(Publicacion => Publicacion.Fecha)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
