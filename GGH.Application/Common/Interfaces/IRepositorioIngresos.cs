using GGH.Application.Features.Gastos.Dtos;
using GGH.Application.Features.Ingresos.Dtos;

namespace GGH.Application.Common.Interfaces
{
    public interface IRepositorioIngresos
    {
        Task<Guid> CrearAsync(Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion, bool recurrente);

        Task ActualizarAsync(Guid ingresoId, Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion, bool recurrente);

        Task EliminarAsync(Guid ingresoId, Guid usuarioId);

        Task<IEnumerable<IngresoDto>> ListarAsync(Guid usuarioId, DateOnly? desde, DateOnly? hasta);
        Task<IngresoDto?> ObtenerPorIdAsync(Guid ingresoId, Guid usuarioId);

        // Reutiliza el mismo CategoriaDto que Gastos, ya que la tabla `categorias` está unificada.
        Task<IEnumerable<CategoriaDto>> ListarCategoriasAsync();
    }
}
