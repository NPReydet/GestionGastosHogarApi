using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerTopCategoriasGastoFamiliar
{
    public class ObtenerTopCategoriasGastoFamiliarQuery : IRequest<IEnumerable<TopCategoriaFamiliarDto>>
    {
        public DateOnly Desde { get; set; }
        public DateOnly Hasta { get; set; }
        public int Limite { get; set; } = 5;
    }
}
