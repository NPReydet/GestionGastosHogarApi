
using GGH.Application.Features.Gastos.Dtos;

namespace GGH.Application.Common.Interfaces
{
    public interface IRepositorioGastos
    {
        Task<Guid> CrearAsync(
            Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion,
            string medioPago, bool recurrente, int? cuotasTotales, int? cuotaActual);

        Task ActualizarAsync(
            Guid gastoId, Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion,
            string medioPago, bool recurrente, int? cuotasTotales, int? cuotaActual);

        Task EliminarAsync(Guid gastoId, Guid usuarioId);

        Task<IEnumerable<GastoDto>> ListarAsync(Guid usuarioId, DateOnly? desde, DateOnly? hasta);
        Task<GastoDto?> ObtenerPorIdAsync(Guid gastoId, Guid usuarioId);
        Task<IEnumerable<CategoriaDto>> ListarCategoriasAsync();
    }
}
