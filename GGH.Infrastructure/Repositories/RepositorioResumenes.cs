using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Resumenes.Dtos;
using GGH.Domain.Exceptions;
using Npgsql;

namespace GGH.Infrastructure.Repositories
{
    public class RepositorioResumenes : IRepositorioResumenes
    {
        private readonly IContextoDapper _contexto;

        public RepositorioResumenes(IContextoDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<ResumenMensualDto>> ObtenerResumenMensualAsync(Guid usuarioId, int mes, int anio)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<ResumenMensualDto>(
                    "sp_resumen_mensual",
                    new { p_usuario_id = usuarioId, p_mes = mes, p_anio = anio });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("MES_INVALIDO") || ex.MessageText.StartsWith("ANIO_INVALIDO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<ResumenAnualDto>> ObtenerResumenAnualAsync(Guid usuarioId, int anio)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<ResumenAnualDto>(
                    "sp_resumen_anual",
                    new { p_usuario_id = usuarioId, p_anio = anio });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("ANIO_INVALIDO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<ComparacionPeriodosDto>> CompararPeriodosAsync(
            Guid usuarioId, DateOnly inicio1, DateOnly fin1, DateOnly inicio2, DateOnly fin2)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<ComparacionPeriodosDto>(
                    "sp_comparar_periodos",
                    new
                    {
                        p_usuario_id = usuarioId,
                        p_inicio_1 = inicio1.ToString("yyyy-MM-dd"),
                        p_fin_1 = fin1.ToString("yyyy-MM-dd"),
                        p_inicio_2 = inicio2.ToString("yyyy-MM-dd"),
                        p_fin_2 = fin2.ToString("yyyy-MM-dd")
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("RANGO_FECHAS_INVALIDO"))
            {
                throw new RangoFechasInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<TopCategoriaDto>> ObtenerTopCategoriasGastoAsync(Guid usuarioId, DateOnly desde, DateOnly hasta, int limite)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<TopCategoriaDto>(
                    "sp_top_categorias_gasto",
                    new
                    {
                        p_usuario_id = usuarioId,
                        p_desde = desde.ToString("yyyy-MM-dd"),
                        p_hasta = hasta.ToString("yyyy-MM-dd"),
                        p_limite = limite
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("RANGO_FECHAS_INVALIDO"))
            {
                throw new RangoFechasInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("LIMITE_INVALIDO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<TopCategoriaDto>> ObtenerTopCategoriasIngresoAsync(Guid usuarioId, DateOnly desde, DateOnly hasta, int limite)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<TopCategoriaDto>(
                    "sp_top_categorias_ingreso",
                    new
                    {
                        p_usuario_id = usuarioId,
                        p_desde = desde.ToString("yyyy-MM-dd"),
                        p_hasta = hasta.ToString("yyyy-MM-dd"),
                        p_limite = limite
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("RANGO_FECHAS_INVALIDO"))
            {
                throw new RangoFechasInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("LIMITE_INVALIDO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }
    }
}
