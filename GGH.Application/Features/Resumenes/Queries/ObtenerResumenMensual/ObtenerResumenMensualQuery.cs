using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerResumenMensual
{
    public class ObtenerResumenMensualQuery : IRequest<IEnumerable<ResumenMensualDto>>
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
}
