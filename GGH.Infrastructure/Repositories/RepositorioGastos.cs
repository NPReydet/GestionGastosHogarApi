using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Gastos.Dtos;
using GGH.Domain.Exceptions;
using Npgsql;

namespace GGH.Infrastructure.Repositories
{
    public class RepositorioGastos : IRepositorioGastos
    {
        private readonly IContextoDapper _contexto;

        public RepositorioGastos(IContextoDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<Guid> CrearAsync(
            Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion,
            string medioPago, bool recurrente, int? cuotasTotales, int? cuotaActual)
        {
            try
            {
                return await _contexto.EjecutarEscalarAsync<Guid>(
                    "sp_registrar_gasto",
                    new
                    {
                        p_usuario_id = usuarioId,
                        p_categoria_id = categoriaId,
                        p_monto = monto,
                        p_fecha = fecha.ToString("yyyy-MM-dd"),
                        p_descripcion = descripcion,
                        p_medio_pago = medioPago,
                        p_recurrente = recurrente,
                        p_cuotas_totales = cuotasTotales,
                        p_cuota_actual = cuotaActual
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("MONTO_INVALIDO"))
            {
                throw new MontoInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("CATEGORIA_INVALIDA"))
            {
                throw new CategoriaInvalidaException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task ActualizarAsync(
            Guid gastoId, Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion,
            string medioPago, bool recurrente, int? cuotasTotales, int? cuotaActual)
        {
            try
            {
                await _contexto.EjecutarAsync(
                    "sp_actualizar_gasto",
                    new
                    {
                        p_gasto_id = gastoId,
                        p_usuario_id = usuarioId,
                        p_categoria_id = categoriaId,
                        p_monto = monto,
                        p_fecha = fecha.ToString("yyyy-MM-dd"),
                        p_descripcion = descripcion,
                        p_medio_pago = medioPago,
                        p_recurrente = recurrente,
                        p_cuotas_totales = cuotasTotales,
                        p_cuota_actual = cuotaActual
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GASTO_NO_ENCONTRADO"))
            {
                throw new GastoNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GASTO_NO_PERTENECE_USUARIO"))
            {
                throw new AccesoNoAutorizadoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("MONTO_INVALIDO"))
            {
                throw new MontoInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("CATEGORIA_INVALIDA"))
            {
                throw new CategoriaInvalidaException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task EliminarAsync(Guid gastoId, Guid usuarioId)
        {
            try
            {
                await _contexto.EjecutarAsync(
                    "sp_eliminar_gasto",
                    new { p_gasto_id = gastoId, p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GASTO_NO_ENCONTRADO"))
            {
                throw new GastoNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GASTO_NO_PERTENECE_USUARIO"))
            {
                throw new AccesoNoAutorizadoException();
            }
        }

        public async Task<IEnumerable<GastoDto>> ListarAsync(Guid usuarioId, DateOnly? desde, DateOnly? hasta)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<GastoDto>(
                    "sp_listar_gastos",
                    new
                    {
                        p_usuario_id = usuarioId,
                        p_desde = desde?.ToString("yyyy-MM-dd"),
                        p_hasta = hasta?.ToString("yyyy-MM-dd")
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
        }

        public async Task<GastoDto?> ObtenerPorIdAsync(Guid gastoId, Guid usuarioId)
        {
            try
            {
                return await _contexto.EjecutarQuerySingleAsync<GastoDto>(
                    "sp_obtener_gasto_por_id",
                    new { p_gasto_id = gastoId, p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GASTO_NO_ENCONTRADO"))
            {
                throw new GastoNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GASTO_NO_PERTENECE_USUARIO"))
            {
                throw new AccesoNoAutorizadoException();
            }
        }

        public async Task<IEnumerable<CategoriaDto>> ListarCategoriasAsync()
        {
            return await _contexto.EjecutarQueryAsync<CategoriaDto>(
                "sp_listar_categorias",
                new { p_tipo = "Gasto" });
        }
    }
}
