using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ListarMiembrosGrupoFamiliar
{
    /// <summary>
    /// No recibe el grupoFamiliarId por parámetro: el Handler lo resuelve
    /// automáticamente a partir del usuario autenticado actual.
    /// </summary>
    public class ListarMiembrosGrupoFamiliarQuery : IRequest<IEnumerable<MiembroGrupoDto>>
    {
    }
}
