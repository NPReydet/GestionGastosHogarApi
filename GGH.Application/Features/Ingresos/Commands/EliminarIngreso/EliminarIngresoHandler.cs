using GGH.Application.Common.Interfaces;
using MediatR;

namespace GGH.Application.Features.Ingresos.Commands.EliminarIngreso
{
    public class EliminarIngresoHandler : IRequestHandler<EliminarIngresoCommand>
    {
        private readonly IRepositorioIngresos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public EliminarIngresoHandler(IRepositorioIngresos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task Handle(EliminarIngresoCommand request, CancellationToken cancellationToken)
        {
            await _repositorio.EliminarAsync(request.IngresoId, _usuarioActual.UsuarioId);
        }
    }
}
