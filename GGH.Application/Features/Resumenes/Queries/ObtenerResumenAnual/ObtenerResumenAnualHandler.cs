using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerResumenAnual
{
    public class ObtenerResumenAnualHandler : IRequestHandler<ObtenerResumenAnualQuery, IEnumerable<ResumenAnualDto>>
    {
        private readonly IRepositorioResumenes _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerResumenAnualHandler(IRepositorioResumenes repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<ResumenAnualDto>> Handle(ObtenerResumenAnualQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ObtenerResumenAnualAsync(_usuarioActual.UsuarioId, request.Anio);
        }
    }
}
