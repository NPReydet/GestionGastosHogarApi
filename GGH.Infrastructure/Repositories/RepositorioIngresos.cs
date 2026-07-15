using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Gastos.Dtos;
using GGH.Application.Features.Ingresos.Dtos;
using GGH.Domain.Exceptions;
using Npgsql;

namespace GGH.Infrastructure.Repositories
{
    public class RepositorioIngresos : IRepositorioIngresos
    {
        private readonly IContextoDapper _contexto;

        public RepositorioIngresos(IContextoDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<Guid> CrearAsync(Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion, bool recurrente)
        {
            try
            {
                return await _contexto.EjecutarEscalarAsync<Guid>(
                    "sp_registrar_ingreso",
                    new
                    {
                        p_usuario_id = usuarioId,
                        p_categoria_id = categoriaId,
                        p_monto = monto,
                        p_fecha = fecha.ToString("yyyy-MM-dd"),
                        p_descripcion = descripcion,
                        p_recurrente = recurrente
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

        public async Task ActualizarAsync(Guid ingresoId, Guid usuarioId, Guid categoriaId, decimal monto, DateOnly fecha, string? descripcion, bool recurrente)
        {
            try
            {
                await _contexto.EjecutarAsync(
                    "sp_actualizar_ingreso",
                    new
                    {
                        p_ingreso_id = ingresoId,
                        p_usuario_id = usuarioId,
                        p_categoria_id = categoriaId,
                        p_monto = monto,
                        p_fecha = fecha.ToString("yyyy-MM-dd"),
                        p_descripcion = descripcion,
                        p_recurrente = recurrente
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("INGRESO_NO_ENCONTRADO"))
            {
                throw new IngresoNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("INGRESO_NO_PERTENECE_USUARIO"))
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

        public async Task EliminarAsync(Guid ingresoId, Guid usuarioId)
        {
            try
            {
                await _contexto.EjecutarAsync(
                    "sp_eliminar_ingreso",
                    new { p_ingreso_id = ingresoId, p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("INGRESO_NO_ENCONTRADO"))
            {
                throw new IngresoNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("INGRESO_NO_PERTENECE_USUARIO"))
            {
                throw new AccesoNoAutorizadoException();
            }
        }

        public async Task<IEnumerable<IngresoDto>> ListarAsync(Guid usuarioId, DateOnly? desde, DateOnly? hasta)
        {
            try
            {
                return await _contexto.EjecutarQueryAsync<IngresoDto>(
                    "sp_listar_ingresos",
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

        public async Task<IngresoDto?> ObtenerPorIdAsync(Guid ingresoId, Guid usuarioId)
        {
            try
            {
                return await _contexto.EjecutarQuerySingleAsync<IngresoDto>(
                    "sp_obtener_ingreso_por_id",
                    new { p_ingreso_id = ingresoId, p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("INGRESO_NO_ENCONTRADO"))
            {
                throw new IngresoNoEncontradoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("INGRESO_NO_PERTENECE_USUARIO"))
            {
                throw new AccesoNoAutorizadoException();
            }
        }

        public async Task<IEnumerable<CategoriaDto>> ListarCategoriasAsync()
        {
            return await _contexto.EjecutarQueryAsync<CategoriaDto>(
                "sp_listar_categorias",
                new { p_tipo = "Ingreso" });
        }
    }
}
