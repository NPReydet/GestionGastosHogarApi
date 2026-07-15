using MediatR;

namespace GGH.Application.Features.Gastos.Commands.EliminarGasto
{
    public class EliminarGastoCommand : IRequest
    {
        public Guid GastoId { get; set; }
    }
}
