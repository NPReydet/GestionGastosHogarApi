using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Application.Features.Ingresos.Commands.ActualizarIngreso
{
    public class ActualizarIngresoCommand : IRequest
    {
        public Guid IngresoId { get; set; }
        public Guid CategoriaId { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; }
    }
}
