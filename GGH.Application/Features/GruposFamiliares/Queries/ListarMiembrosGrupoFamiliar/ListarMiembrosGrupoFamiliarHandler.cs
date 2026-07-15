using GGH.Application.Common.Interfaces;
using GGH.Application.Features.GruposFamiliares.Dtos;
using GGH.Domain.Exceptions;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ListarMiembrosGrupoFamiliar
{
    public class ListarMiembrosGrupoFamiliarHandler : IRequestHandler<ListarMiembrosGrupoFamiliarQuery, IEnumerable<MiembroGrupoDto>>
    {
        private readonly IRepositorioGruposFamiliares _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ListarMiembrosGrupoFamiliarHandler(IRepositorioGruposFamiliares repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IEnumerable<MiembroGrupoDto>> Handle(ListarMiembrosGrupoFamiliarQuery request, CancellationToken cancellationToken)
        {
            var grupoFamiliarId = await _repositorio.ObtenerGrupoFamiliarIdDeUsuarioAsync(_usuarioActual.UsuarioId);

            if (grupoFamiliarId is null)
            {
                throw new ParametroInvalidoException("No perteneces a ningún grupo familiar.");
            }

            return await _repositorio.ListarMiembrosAsync(grupoFamiliarId.Value);
        }
    }
}
