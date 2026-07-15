using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Gastos.Queries.ListarGastos
{
    public class ListarGastosQuery : IRequest<IEnumerable<GastoDto>>
    {
        public DateOnly? Desde { get; set; }
        public DateOnly? Hasta { get; set; }
    }
}
