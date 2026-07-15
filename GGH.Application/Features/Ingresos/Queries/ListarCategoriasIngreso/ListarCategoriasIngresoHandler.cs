using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Ingresos.Queries.ListarCategoriasIngreso
{
    public class ListarCategoriasIngresoHandler : IRequestHandler<ListarCategoriasIngresoQuery, IEnumerable<CategoriaDto>>
    {
        private readonly IRepositorioIngresos _repositorio;

        public ListarCategoriasIngresoHandler(IRepositorioIngresos repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<CategoriaDto>> Handle(ListarCategoriasIngresoQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarCategoriasAsync();
        }
    }
}
