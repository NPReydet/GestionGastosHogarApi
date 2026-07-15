using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Ingresos.Dtos;
using MediatR;

namespace GGH.Application.Features.Ingresos.Queries.ObtenerIngresoPorId
{
    public class ObtenerIngresoPorIdHandler : IRequestHandler<ObtenerIngresoPorIdQuery, IngresoDto?>
    {
        private readonly IRepositorioIngresos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public ObtenerIngresoPorIdHandler(IRepositorioIngresos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<IngresoDto?> Handle(ObtenerIngresoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repositorio.ObtenerPorIdAsync(request.IngresoId, _usuarioActual.UsuarioId);
        }
    }
}
