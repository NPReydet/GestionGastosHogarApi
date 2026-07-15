using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerResumenMensualFamiliar
{
    public class ObtenerResumenMensualFamiliarQuery : IRequest<IEnumerable<ResumenMensualFamiliarDto>>
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
}
