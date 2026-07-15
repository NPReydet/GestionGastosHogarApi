using GGH.Application.Common.Interfaces;
using MediatR;

namespace GGH.Application.Features.Ingresos.Commands.ActualizarIngreso
{
    public class ActualizarIngresoHandler : IRequestHandler<ActualizarIngresoCommand>
    {
        private readonly IRepositorioIngresos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ActualizarIngresoHandler(IRepositorioIngresos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task Handle(ActualizarIngresoCommand request, CancellationToken cancellationToken)
        {
            await _repositorio.ActualizarAsync(
                request.IngresoId, _usuarioActual.UsuarioId, request.CategoriaId, request.Monto,
                request.Fecha, request.Descripcion, request.Recurrente);
        }
    }
}
