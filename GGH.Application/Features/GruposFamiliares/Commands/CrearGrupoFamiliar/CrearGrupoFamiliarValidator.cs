using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Commands.CrearGrupoFamiliar
{
    public class CrearGrupoFamiliarCommand : IRequest<GrupoFamiliarCreadoDto>
    {
        public string? NombreGrupo { get; set; }
    }
}
