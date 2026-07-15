using GGH.Application.Features.Ingresos.Dtos;
using MediatR;

namespace GGH.Application.Features.Ingresos.Queries.ObtenerIngresoPorId
{
    public class ObtenerIngresoPorIdQuery : IRequest<IngresoDto?>
    {
        public Guid IngresoId { get; set; }
    }
}
