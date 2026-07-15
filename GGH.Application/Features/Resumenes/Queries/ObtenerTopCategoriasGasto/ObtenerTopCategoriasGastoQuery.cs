using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerTopCategoriasGasto
{
    public class ObtenerTopCategoriasGastoQuery : IRequest<IEnumerable<TopCategoriaDto>>
    {
        public DateOnly Desde { get; set; }
        public DateOnly Hasta { get; set; }
        public int Limite { get; set; } = 5;
    }
}
