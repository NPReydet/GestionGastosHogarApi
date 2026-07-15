using GGH.Application.Features.Ingresos.Dtos;
using MediatR;

namespace GGH.Application.Features.Ingresos.Queries.ListarIngresos
{
    public class ListarIngresosQuery : IRequest<IEnumerable<IngresoDto>>
    {
        public DateOnly? Desde { get; set; }
        public DateOnly? Hasta { get; set; }
    }
}
