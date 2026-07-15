using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Gastos.Queries.ObtenerGastoPorId
{
    public class ObtenerGastoPorIdHandler : IRequestHandler<ObtenerGastoPorIdQuery, GastoDto?>
    {
        private readonly IRepositorioGastos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerGastoPorIdHandler(IRepositorioGastos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<GastoDto?> Handle(ObtenerGastoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ObtenerPorIdAsync(request.GastoId, _usuarioActual.UsuarioId);
        }
    }
}
