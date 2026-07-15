using GGH.Application.Common.Interfaces;
using MediatR;

namespace GGH.Application.Features.Gastos.Commands.EliminarGasto
{
    public class EliminarGastoHandler : IRequestHandler<EliminarGastoCommand>
    {
        private readonly IRepositorioGastos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public EliminarGastoHandler(IRepositorioGastos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task Handle(EliminarGastoCommand request, CancellationToken cancellationToken)
        {
            await _repositorio.EliminarAsync(request.GastoId, _usuarioActual.UsuarioId);
        }
    }
}
