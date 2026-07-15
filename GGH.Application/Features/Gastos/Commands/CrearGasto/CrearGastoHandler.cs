using GGH.Application.Common.Interfaces;
using MediatR;

namespace GGH.Application.Features.Gastos.Commands.CrearGasto
{
    public class CrearGastoHandler : IRequestHandler<CrearGastoCommand, Guid>
    {
        private readonly IRepositorioGastos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public CrearGastoHandler(IRepositorioGastos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<Guid> Handle(CrearGastoCommand request, CancellationToken cancellationToken)
        {
            return await _repositorio.CrearAsync(
                _usuarioActual.UsuarioId, request.CategoriaId, request.Monto, request.Fecha, request.Descripcion,
                request.MedioPago, request.Recurrente, request.CuotasTotales, request.CuotaActual);
        }
    }
}
