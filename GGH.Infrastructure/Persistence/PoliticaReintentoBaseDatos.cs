using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using Polly.Retry;

namespace GGH.Infrastructure.Persistence
{
    public static class PoliticaReintentoBaseDatos
    {
        private const int NumeroReintentos = 3;

        /// <summary>
        /// 3 reintentos con espera creciente (2s, 4s, 6s). Se activa ante
        /// errores de conexión/timeout con PostgreSQL, no ante errores de
        /// negocio (esos vienen como excepciones normales de la función SQL).
        /// </summary>
        public static AsyncRetryPolicy CrearPolitica(ILogger logger)
        {
            return Policy
                .Handle<NpgsqlException>(EsErrorDeConexion)
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    retryCount: NumeroReintentos,
                    sleepDurationProvider: intento => TimeSpan.FromSeconds(intento * 2),
                    onRetry: (excepcion, espera, intento, _) =>
                    {
                        logger.LogWarning(
                            excepcion,
                            "Intento {Intento}/{Total} fallido al conectar con PostgreSQL. Reintentando en {Espera}s.",
                            intento, NumeroReintentos, espera.TotalSeconds);
                    });
        }

        /// <summary>
        /// Solo reintentamos ante errores transitorios de red/conexión,
        /// no ante errores de sintaxis SQL o violaciones de constraint
        /// (esos deben fallar inmediatamente, reintentarlos no ayuda).
        /// </summary>
        private static bool EsErrorDeConexion(NpgsqlException ex) =>
            ex.IsTransient || ex.InnerException is TimeoutException or System.Net.Sockets.SocketException;
    }
}
