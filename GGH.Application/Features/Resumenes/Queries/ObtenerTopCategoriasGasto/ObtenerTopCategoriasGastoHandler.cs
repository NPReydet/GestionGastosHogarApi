using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerTopCategoriasGasto
{
    public class ObtenerTopCategoriasGastoHandler : IRequestHandler<ObtenerTopCategoriasGastoQuery, IEnumerable<TopCategoriaDto>>
    {
        private readonly IRepositorioResumenes _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerTopCategoriasGastoHandler(IRepositorioResumenes repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<TopCategoriaDto>> Handle(ObtenerTopCategoriasGastoQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ObtenerTopCategoriasGastoAsync(_usuarioActual.UsuarioId, request.Desde, request.Hasta, request.Limite);
        }
    }
}
