using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Gastos.Dtos;
using MediatR;

namespace GGH.Application.Features.Gastos.Queries.ListarCategoriasGasto
{
    public class ListarCategoriasGastoHandler : IRequestHandler<ListarCategoriasGastoQuery, IEnumerable<CategoriaDto>>
    {
        private readonly IRepositorioGastos _repositorio;

        public ListarCategoriasGastoHandler(IRepositorioGastos repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<CategoriaDto>> Handle(ListarCategoriasGastoQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarCategoriasAsync();
        }
    }
}
