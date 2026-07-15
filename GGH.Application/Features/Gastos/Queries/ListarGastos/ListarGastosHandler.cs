using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Gastos.Queries.ListarGastos
{
    public class ListarGastosHandler : IRequestHandler<ListarGastosQuery, IEnumerable<GastoDto>>
    {
        private readonly IRepositorioGastos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ListarGastosHandler(IRepositorioGastos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<GastoDto>> Handle(ListarGastosQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarAsync(_usuarioActual.UsuarioId, request.Desde, request.Hasta);
        }
    }
}
