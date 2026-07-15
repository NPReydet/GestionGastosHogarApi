using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.CompararPeriodosFamiliar
{
    public class CompararPeriodosFamiliarQuery : IRequest<IEnumerable<ComparacionPeriodosFamiliarDto>>
    {
        public DateOnly Inicio1 { get; set; }
        public DateOnly Fin1 { get; set; }
        public DateOnly Inicio2 { get; set; }
        public DateOnly Fin2 { get; set; }
    }
}
