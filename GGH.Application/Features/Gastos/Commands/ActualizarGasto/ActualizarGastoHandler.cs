using GGH.Application.Common.Interfaces;
using MediatR;

namespace GGH.Application.Features.Gastos.Commands.ActualizarGasto
{
    public class ActualizarGastoHandler : IRequestHandler<ActualizarGastoCommand>
    {
        private readonly IRepositorioGastos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ActualizarGastoHandler(IRepositorioGastos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task Handle(ActualizarGastoCommand request, CancellationToken cancellationToken)
        {
            await _repositorio.ActualizarAsync(
                request.GastoId, _usuarioActual.UsuarioId, request.CategoriaId, request.Monto, request.Fecha,
                request.Descripcion, request.MedioPago, request.Recurrente, request.CuotasTotales, request.CuotaActual);
        }
    }
}
