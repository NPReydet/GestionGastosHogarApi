using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.CompararPeriodos
{
    public class CompararPeriodosHandler : IRequestHandler<CompararPeriodosQuery, IEnumerable<ComparacionPeriodosDto>>
    {
        private readonly IRepositorioResumenes _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public CompararPeriodosHandler(IRepositorioResumenes repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<ComparacionPeriodosDto>> Handle(CompararPeriodosQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.CompararPeriodosAsync(
                _usuarioActual.UsuarioId, request.Inicio1, request.Fin1, request.Inicio2, request.Fin2);
        }
    }
}
