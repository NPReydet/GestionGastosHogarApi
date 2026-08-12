using Dapper;
using GGH.Application.Common.Interfaces;
using GGH.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly.Retry;
using System.Data;

namespace GGH.Infrastructure.Persistence
{
    public class ContextoDapper : IContextoDapper
    {
        private readonly string _connectionString;
        private readonly AsyncRetryPolicy _politicaReintento;
        private readonly ILogger<ContextoDapper> _logger;

        public ContextoDapper(IConfiguration configuracion, ILogger<ContextoDapper> logger)
        {
            _connectionString = configuracion.GetConnectionString("PostgreSQL")
                ?? throw new InvalidOperationException("No se configuró la connection string 'PostgreSQL'.");
            _logger = logger;
            _politicaReintento = PoliticaReintentoBaseDatos.CrearPolitica(logger);
        }

        public async Task<IEnumerable<T>> EjecutarQueryAsync<T>(string spNombre, object? parametros = null)
        {
            var sql = ConstruirLlamadaFuncion(spNombre, parametros);

            var resultado = await EjecutarConPoliticaAsync(async () =>
            {
                using var conexion = new NpgsqlConnection(_connectionString);
                IEnumerable<T>? filas = await conexion.QueryAsync<T>(sql, parametros);
                return filas;
            });

            return resultado ?? Enumerable.Empty<T>();
        }

        public async Task<T?> EjecutarQuerySingleAsync<T>(string spNombre, object? parametros = null)
        {
            var sql = ConstruirLlamadaFuncion(spNombre, parametros);

            return await EjecutarConPoliticaAsync(async () =>
            {
                using var conexion = new NpgsqlConnection(_connectionString);
                return await conexion.QuerySingleOrDefaultAsync<T>(sql, parametros);
            });
        }

        public async Task<T> EjecutarEscalarAsync<T>(string spNombre, object? parametros = null)
        {
            var sql = ConstruirLlamadaFuncion(spNombre, parametros);

            var resultado = await EjecutarConPoliticaAsync(async () =>
            {
                using var conexion = new NpgsqlConnection(_connectionString);
                T? valor = await conexion.ExecuteScalarAsync<T>(sql, parametros);
                return valor;
            });

            return resultado is null
                ? throw new InvalidOperationException($"La función '{spNombre}' no devolvió ningún valor escalar.")
                : resultado;
        }

        public async Task EjecutarAsync(string spNombre, object? parametros = null)
        {
            var sql = ConstruirLlamadaFuncionVoid(spNombre, parametros);

            await EjecutarConPoliticaAsync<object?>(async () =>
            {
                using var conexion = new NpgsqlConnection(_connectionString);
                await conexion.ExecuteAsync(sql, parametros);
                return null;
            });
        }

        /// <summary>
        /// Construye "SELECT * FROM funcion(p1 => @p1, p2 => @p2, ...)" a partir
        /// de los nombres de parámetros. Acepta tanto objetos anónimos (el caso
        /// más común) como Dapper.DynamicParameters (necesario cuando hay que
        /// forzar un tipo explícito, ej: DbType.Date para que Npgsql no confunda
        /// un DateTime con 'timestamp' en vez de 'date'). En ambos casos, los
        /// nombres DEBEN coincidir exactamente con los parámetros de la función SQL.
        /// </summary>
        private static string ConstruirLlamadaFuncion(string nombreFuncion, object? parametros)
        {
            if (parametros is null)
            {
                return $"SELECT * FROM {nombreFuncion}()";
            }

            var nombresParametros = parametros is DynamicParameters dynamicParams
                ? dynamicParams.ParameterNames
                : parametros.GetType().GetProperties().Select(propiedad => propiedad.Name);

            var argumentos = string.Join(", ", nombresParametros.Select(nombre => $"{nombre} => @{nombre}"));

            return $"SELECT * FROM {nombreFuncion}({argumentos})";
        }

        /// <summary>
        /// Igual que ConstruirLlamadaFuncion, pero SIN "* FROM" — necesario para
        /// funciones que retornan VOID, ya que Postgres no permite usarlas como
        /// función de tabla en un FROM.
        /// </summary>
        private static string ConstruirLlamadaFuncionVoid(string nombreFuncion, object? parametros)
        {
            if (parametros is null)
            {
                return $"SELECT {nombreFuncion}()";
            }

            var nombresParametros = parametros is DynamicParameters dynamicParams
                ? dynamicParams.ParameterNames
                : parametros.GetType().GetProperties().Select(propiedad => propiedad.Name);

            var argumentos = string.Join(", ", nombresParametros.Select(nombre => $"{nombre} => @{nombre}"));

            return $"SELECT {nombreFuncion}({argumentos})";
        }

        private async Task<T?> EjecutarConPoliticaAsync<T>(Func<Task<T?>> operacion)
        {
            try
            {
                return await _politicaReintento.ExecuteAsync(operacion);
            }
            catch (PostgresException)
            {
                // Errores de negocio lanzados desde las funciones SQL (RAISE EXCEPTION 'CODIGO: mensaje')
                // llegan como PostgresException. Ojo: PostgresException hereda de NpgsqlException, así
                // que este catch DEBE ir antes del catch(NpgsqlException) genérico, o nunca se alcanzaría.
                // Se deja propagar tal cual para que el repositorio la traduzca a una excepción de dominio.
                throw;
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Se agotaron los reintentos de conexión a PostgreSQL.");
                throw new ErrorComunicacionBaseDatosException();
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Timeout al conectar con PostgreSQL tras los reintentos.");
                throw new ErrorComunicacionBaseDatosException();
            }
        }
    }

}
