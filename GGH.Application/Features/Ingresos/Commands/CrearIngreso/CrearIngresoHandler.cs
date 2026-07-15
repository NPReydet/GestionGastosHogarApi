using GGH.Application.Common.Interfaces;
using MediatR;

namespace GGH.Application.Features.Ingresos.Commands.CrearIngreso
{
    public class CrearIngresoHandler : IRequestHandler<CrearIngresoCommand, Guid>
    {
        private readonly IRepositorioIngresos _repositorio;
        private readonly IUsuarioActual _usuarioActual;

        public CrearIngresoHandler(IRepositorioIngresos repositorio, IUsuarioActual usuarioActual)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
        }

        public async Task<Guid> Handle(CrearIngresoCommand request, CancellationToken cancellationToken)
        {
            return await _repositorio.CrearAsync(
                _usuarioActual.UsuarioId, request.CategoriaId, request.Monto, request.Fecha,
                request.Descripcion, request.Recurrente);
        }
    }
}
