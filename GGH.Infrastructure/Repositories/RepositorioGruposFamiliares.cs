using GGH.Application.Common.Interfaces;
using GGH.Application.Features.GruposFamiliares.Dtos;
using GGH.Domain.Exceptions;
using Npgsql;

namespace GGH.Infrastructure.Repositories
{
    public class RepositorioGruposFamiliares : IRepositorioGruposFamiliares
    {
        private readonly IContextoDapper _contexto;

        public RepositorioGruposFamiliares(IContextoDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<GrupoFamiliarCreadoDto> CrearAsync(Guid usuarioId, string? nombreGrupo)
        {
            try
            {
                return await _contexto.EjecutarQuerySingleAsync<GrupoFamiliarCreadoDto>(
                    "sp_crear_grupo_familiar",
                    new { p_usuario_id = usuarioId, p_nombre_grupo = nombreGrupo })
                    ?? throw new InvalidOperationException("sp_crear_grupo_familiar no devolvió resultado.");
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_YA_EN_GRUPO"))
            {
                throw new UsuarioYaEnGrupoException();
            }
        }

        public async Task<Guid> UnirseAsync(Guid usuarioId, string codigo)
        {
            try
            {
                return await _contexto.EjecutarEscalarAsync<Guid>(
                    "sp_unirse_grupo_familiar",
                    new { p_usuario_id = usuarioId, p_codigo = codigo });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_YA_EN_GRUPO"))
            {
                throw new UsuarioYaEnGrupoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("CODIGO_INVALIDO"))
            {
                throw new CodigoGrupoInvalidoException(ex.MessageText.Replace("CODIGO_INVALIDO: ", string.Empty));
            }
        }

        public async Task SalirAsync(Guid usuarioId)
        {
            try
            {
                await _contexto.EjecutarAsync(
                    "sp_salir_grupo_familiar",
                    new { p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_SIN_GRUPO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Replace("USUARIO_SIN_GRUPO: ", string.Empty));
            }
        }

        public async Task<Guid?> ObtenerGrupoFamiliarIdDeUsuarioAsync(Guid usuarioId)
        {
            try
            {
                return await _contexto.EjecutarQuerySingleAsync<Guid?>(
                    "sp_obtener_grupo_familiar_usuario",
                    new { p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
        }

        public async Task<IEnumerable<MiembroGrupoDto>> ListarMiembrosAsync(Guid grupoFamiliarId)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<MiembroGrupoDto>(
                    "sp_listar_miembros_grupo_familiar",
                    new { p_grupo_familiar_id = grupoFamiliarId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GRUPO_FAMILIAR_NO_ENCONTRADO"))
            {
                throw new GrupoFamiliarNoEncontradoException();
            }
        }

        public async Task<IEnumerable<ResumenMensualFamiliarDto>> ObtenerResumenMensualAsync(Guid grupoFamiliarId, int mes, int anio)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<ResumenMensualFamiliarDto>(
                    "sp_resumen_mensual_familiar",
                    new { p_grupo_familiar_id = grupoFamiliarId, p_mes = mes, p_anio = anio });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GRUPO_FAMILIAR_NO_ENCONTRADO"))
            {
                throw new GrupoFamiliarNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("MES_INVALIDO") || ex.MessageText.StartsWith("ANIO_INVALIDO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<ResumenAnualFamiliarDto>> ObtenerResumenAnualAsync(Guid grupoFamiliarId, int anio)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<ResumenAnualFamiliarDto>(
                    "sp_resumen_anual_familiar",
                    new { p_grupo_familiar_id = grupoFamiliarId, p_anio = anio });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GRUPO_FAMILIAR_NO_ENCONTRADO"))
            {
                throw new GrupoFamiliarNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("ANIO_INVALIDO"))
            {
                throw new ParametroInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<ComparacionPeriodosFamiliarDto>> CompararPeriodosAsync(
            Guid grupoFamiliarId, DateOnly inicio1, DateOnly fin1, DateOnly inicio2, DateOnly fin2)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<ComparacionPeriodosFamiliarDto>(
                    "sp_comparar_periodos_familiar",
                    new
                    {
                        p_grupo_familiar_id = grupoFamiliarId,
                        p_inicio_1 = inicio1.ToString("yyyy-MM-dd"),
                        p_fin_1 = fin1.ToString("yyyy-MM-dd"),
                        p_inicio_2 = inicio2.ToString("yyyy-MM-dd"),
                        p_fin_2 = fin2.ToString("yyyy-MM-dd")
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GRUPO_FAMILIAR_NO_ENCONTRADO"))
            {
                throw new GrupoFamiliarNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("RANGO_FECHAS_INVALIDO"))
            {
                throw new RangoFechasInvalidoException(ex.MessageText.Split(": ", 2)[1]);
            }
        }

        public async Task<IEnumerable<TopCategoriaFamiliarDto>> ObtenerTopCategoriasGastoAsync(
            Guid grupoFamiliarId, DateOnly desde, DateOnly hasta, int limite)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<TopCategoriaFamiliarDto>(
                    "sp_top_categorias_gasto_familiar",
                    new
                    {
                        p_grupo_familiar_id = grupoFamiliarId,
                        p_desde = desde.ToString("yyyy-MM-dd"),
                        p_hasta = hasta.ToString("yyyy-MM-dd"),
                        p_limite = limite
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GRUPO_FAMILIAR_NO_ENCONTRADO"))
            {
                throw new GrupoFamiliarNoEncontradoException();
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

        public async Task<IEnumerable<TopCategoriaFamiliarDto>> ObtenerTopCategoriasIngresoAsync(
            Guid grupoFamiliarId, DateOnly desde, DateOnly hasta, int limite)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<TopCategoriaFamiliarDto>(
                    "sp_top_categorias_ingreso_familiar",
                    new
                    {
                        p_grupo_familiar_id = grupoFamiliarId,
                        p_desde = desde.ToString("yyyy-MM-dd"),
                        p_hasta = hasta.ToString("yyyy-MM-dd"),
                        p_limite = limite
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("GRUPO_FAMILIAR_NO_ENCONTRADO"))
            {
                throw new GrupoFamiliarNoEncontradoException();
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
