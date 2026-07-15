using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerResumenAnual
{
    public class ObtenerResumenAnualQuery : IRequest<IEnumerable<ResumenAnualDto>>
    {
        public int Anio { get; set; }
    }
}
