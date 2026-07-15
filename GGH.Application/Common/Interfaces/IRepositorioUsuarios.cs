using GGH.Domain.Entities;

namespace GGH.Application.Common.Interfaces
{
    public interface IRepositorioUsuarios
    {
        Task<Usuario?> ObtenerPorRutAsync(long rut, char dv);
        Task<Usuario?> ObtenerPorAuth0IdAsync(string auth0Id);

        Task<Guid> CrearAsync(
            long rut,
            char dv,
            string nombres,
            string apellidoPaterno,
            string apellidoMaterno,
            string email,
            string passwordHash,
            DateOnly fechaNacimiento,
            string direccion);

        /// <summary>
        /// Derecho al olvido: borrado físico total del usuario y sus datos
        /// (gastos, ingresos, datos sensibles). Ejecuta sp_eliminar_usuario_completo.
        /// </summary>
        Task EliminarCuentaAsync(Guid usuarioId);
    }
}
