using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerResumenAnualFamiliar
{
    public class ObtenerResumenAnualFamiliarQuery : IRequest<IEnumerable<ResumenAnualFamiliarDto>>
    {
        public int Anio { get; set; }
    }

}
