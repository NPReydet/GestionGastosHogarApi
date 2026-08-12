using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using Polly.Retry;

namespace GGH.Infrastructure.Persistence
{
    public static class PoliticaReintentoBaseDatos
    {
        private const int NumeroReintentos = 3;

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

      
        private static bool EsErrorDeConexion(NpgsqlException ex) =>
            ex.IsTransient || ex.InnerException is TimeoutException or System.Net.Sockets.SocketException;
    }
}
