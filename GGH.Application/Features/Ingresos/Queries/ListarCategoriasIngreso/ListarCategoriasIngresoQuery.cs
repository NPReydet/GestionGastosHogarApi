using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Ingresos.Queries.ListarCategoriasIngreso
{
    public class ListarCategoriasIngresoQuery : IRequest<IEnumerable<CategoriaDto>>
    {
    }

}
