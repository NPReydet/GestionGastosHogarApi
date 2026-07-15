using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerTopCategoriasIngresoFamiliar
{
    public class ObtenerTopCategoriasIngresoFamiliarQuery : IRequest<IEnumerable<TopCategoriaFamiliarDto>>
    {
        public DateOnly Desde { get; set; }
        public DateOnly Hasta { get; set; }
        public int Limite { get; set; } = 5;
    }
}
