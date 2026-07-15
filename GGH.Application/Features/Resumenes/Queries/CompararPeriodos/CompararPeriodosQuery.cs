using GGH.Application.Features.Resumenes.Dtos;
using MediatR;

namespace GGH.Application.Features.Resumenes.Queries.CompararPeriodos
{
    public class CompararPeriodosQuery : IRequest<IEnumerable<ComparacionPeriodosDto>>
    {
        public DateOnly Inicio1 { get; set; }
        public DateOnly Fin1 { get; set; }
        public DateOnly Inicio2 { get; set; }
        public DateOnly Fin2 { get; set; }
    }
}
