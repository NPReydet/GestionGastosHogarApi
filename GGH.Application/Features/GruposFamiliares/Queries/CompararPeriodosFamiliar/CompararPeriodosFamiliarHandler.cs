using GGH.Application.Common.Interfaces;
using GGH.Application.Features.GruposFamiliares.Dtos;
using GGH.Domain.Exceptions;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.CompararPeriodosFamiliar
{
    public class CompararPeriodosFamiliarHandler
    : IRequestHandler<CompararPeriodosFamiliarQuery, IEnumerable<ComparacionPeriodosFamiliarDto>>
    {
        private readonly IRepositorioGruposFamiliares _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public CompararPeriodosFamiliarHandler(IRepositorioGruposFamiliares repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<ComparacionPeriodosFamiliarDto>> Handle(
            CompararPeriodosFamiliarQuery request, CancellationToken cancellationToken)
        {
            var grupoFamiliarId = await _repositorio.ObtenerGrupoFamiliarIdDeUsuarioAsync(_usuarioActual.UsuarioId);

            if (grupoFamiliarId is null)
            {
                throw new ParametroInvalidoException("No perteneces a ningún grupo familiar.");
            }

            return await _repositorio.CompararPeriodosAsync(
                grupoFamiliarId.Value, request.Inicio1, request.Fin1, request.Inicio2, request.Fin2);
        }
    }
}
