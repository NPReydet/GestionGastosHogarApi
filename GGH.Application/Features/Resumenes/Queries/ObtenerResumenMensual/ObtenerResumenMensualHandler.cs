using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerResumenMensual
{
    public class ObtenerResumenMensualHandler : IRequestHandler<ObtenerResumenMensualQuery, IEnumerable<ResumenMensualDto>>
    {
        private readonly IRepositorioResumenes _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerResumenMensualHandler(IRepositorioResumenes repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<ResumenMensualDto>> Handle(ObtenerResumenMensualQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ObtenerResumenMensualAsync(_usuarioActual.UsuarioId, request.Mes, request.Anio);
        }
    }
}
