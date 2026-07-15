using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Gastos.Queries.ListarCategoriasGasto
{
    public class ListarCategoriasGastoQuery : IRequest<IEnumerable<CategoriaDto>>
    {
    }
}
