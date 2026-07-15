
using GGH.Application.Features.GruposFamiliares.Dtos;

namespace GGH.Application.Common.Interfaces
{
    public interface IRepositorioGruposFamiliares
    {
        Task<GrupoFamiliarCreadoDto> CrearAsync(Guid usuarioId, string? nombreGrupo);
        Task<Guid> UnirseAsync(Guid usuarioId, string codigo);
        Task SalirAsync(Guid usuarioId);
        Task<Guid?> ObtenerGrupoFamiliarIdDeUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<MiembroGrupoDto>> ListarMiembrosAsync(Guid grupoFamiliarId);

        Task<IEnumerable<ResumenMensualFamiliarDto>> ObtenerResumenMensualAsync(Guid grupoFamiliarId, int mes, int anio);
        Task<IEnumerable<ResumenAnualFamiliarDto>> ObtenerResumenAnualAsync(Guid grupoFamiliarId, int anio);
        Task<IEnumerable<ComparacionPeriodosFamiliarDto>> CompararPeriodosAsync(
            Guid grupoFamiliarId, DateOnly inicio1, DateOnly fin1, DateOnly inicio2, DateOnly fin2);
        Task<IEnumerable<TopCategoriaFamiliarDto>> ObtenerTopCategoriasGastoAsync(
            Guid grupoFamiliarId, DateOnly desde, DateOnly hasta, int limite);
        Task<IEnumerable<TopCategoriaFamiliarDto>> ObtenerTopCategoriasIngresoAsync(
            Guid grupoFamiliarId, DateOnly desde, DateOnly hasta, int limite);
    }
}
