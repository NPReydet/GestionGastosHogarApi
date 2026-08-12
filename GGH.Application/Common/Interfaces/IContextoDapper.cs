
namespace GGH.Application.Common.Interfaces
{
    public interface IContextoDapper
    {
        Task<IEnumerable<T>> EjecutarQueryAsync<T>(string spNombre, object? parametros = null);
        Task<T?> EjecutarQuerySingleAsync<T>(string spNombre, object? parametros = null);
        Task<T> EjecutarEscalarAsync<T>(string spNombre, object? parametros = null);
        Task EjecutarAsync(string spNombre, object? parametros = null);
    }
}
