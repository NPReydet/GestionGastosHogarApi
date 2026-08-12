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

        Task EliminarCuentaAsync(Guid usuarioId);
    }
}
