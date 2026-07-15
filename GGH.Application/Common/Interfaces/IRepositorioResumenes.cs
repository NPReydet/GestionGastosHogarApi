using GGH.Application.Features.Resumenes.Dtos;

namespace GGH.Application.Common.Interfaces
{
    public interface IRepositorioResumenes
    {
        Task<IEnumerable<ResumenMensualDto>> ObtenerResumenMensualAsync(Guid usuarioId, int mes, int anio);
        Task<IEnumerable<ResumenAnualDto>> ObtenerResumenAnualAsync(Guid usuarioId, int anio);
        Task<IEnumerable<ComparacionPeriodosDto>> CompararPeriodosAsync(
            Guid usuarioId, DateOnly inicio1, DateOnly fin1, DateOnly inicio2, DateOnly fin2);
        Task<IEnumerable<TopCategoriaDto>> ObtenerTopCategoriasGastoAsync(Guid usuarioId, DateOnly desde, DateOnly hasta, int limite);
        Task<IEnumerable<TopCategoriaDto>> ObtenerTopCategoriasIngresoAsync(Guid usuarioId, DateOnly desde, DateOnly hasta, int limite);
    }
}
