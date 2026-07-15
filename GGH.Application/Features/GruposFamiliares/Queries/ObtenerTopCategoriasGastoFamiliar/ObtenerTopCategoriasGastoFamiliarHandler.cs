using GGH.Application.Common.Interfaces;
using GGH.Application.Features.GruposFamiliares.Dtos;
using GGH.Domain.Exceptions;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerTopCategoriasGastoFamiliar
{
    public class ObtenerTopCategoriasGastoFamiliarHandler
    : IRequestHandler<ObtenerTopCategoriasGastoFamiliarQuery, IEnumerable<TopCategoriaFamiliarDto>>
    {
        private readonly IRepositorioGruposFamiliares _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerTopCategoriasGastoFamiliarHandler(IRepositorioGruposFamiliares repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<TopCategoriaFamiliarDto>> Handle(
            ObtenerTopCategoriasGastoFamiliarQuery request, CancellationToken cancellationToken)
        {
            var grupoFamiliarId = await _repositorio.ObtenerGrupoFamiliarIdDeUsuarioAsync(_usuarioActual.UsuarioId);

            if (grupoFamiliarId is null)
            {
                throw new ParametroInvalidoException("No perteneces a ningún grupo familiar.");
            }

            return await _repositorio.ObtenerTopCategoriasGastoAsync(
                grupoFamiliarId.Value, request.Desde, request.Hasta, request.Limite);
        }
    }
}
