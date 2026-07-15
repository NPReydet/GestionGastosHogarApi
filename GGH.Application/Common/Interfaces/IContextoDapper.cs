
namespace GGH.Application.Common.Interfaces
{
    /// <summary>
    /// Abstrae el acceso a Dapper + PostgreSQL. Implementada en Infrastructure,
    /// incluyendo la política de reintentos (Polly) de forma centralizada.
    /// </summary>
    public interface IContextoDapper
    {
        Task<IEnumerable<T>> EjecutarQueryAsync<T>(string spNombre, object? parametros = null);
        Task<T?> EjecutarQuerySingleAsync<T>(string spNombre, object? parametros = null);
        Task<T> EjecutarEscalarAsync<T>(string spNombre, object? parametros = null);
        Task EjecutarAsync(string spNombre, object? parametros = null);
    }
}
