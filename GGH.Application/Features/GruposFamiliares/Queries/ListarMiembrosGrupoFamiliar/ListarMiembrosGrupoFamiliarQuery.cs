using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ListarMiembrosGrupoFamiliar
{
    public class ListarMiembrosGrupoFamiliarQuery : IRequest<IEnumerable<MiembroGrupoDto>>
    {
    }
}
