using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Gastos.Queries.ObtenerGastoPorId
{
    public class ObtenerGastoPorIdQuery : IRequest<GastoDto?>
    {
        public Guid GastoId { get; set; }
    }
}
