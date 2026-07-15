using MediatR;

namespace GGH.Application.Features.Ingresos.Commands.EliminarIngreso
{
    public class EliminarIngresoCommand : IRequest
    {
        public Guid IngresoId { get; set; }
    }
}
