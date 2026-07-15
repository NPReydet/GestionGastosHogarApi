using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Ingresos.Dtos;
using MediatR;

namespace GGH.Application.Features.Ingresos.Queries.ListarIngresos
{
    public class ListarIngresosHandler : IRequestHandler<ListarIngresosQuery, IEnumerable<IngresoDto>>
    {
        private readonly IRepositorioIngresos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ListarIngresosHandler(IRepositorioIngresos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<IngresoDto>> Handle(ListarIngresosQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarAsync(_usuarioActual.UsuarioId, request.Desde, request.Hasta);
        }
    }
}
