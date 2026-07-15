
using System.Security.Claims;

namespace GGH.Application.Common.Interfaces
{
    /// <summary>
    /// Expone los datos del usuario autenticado en el request actual,
    /// extraídos de los claims del JWT validado por el middleware de
    /// autenticación. Implementado en Infrastructure usando IHttpContextAccessor.
    /// </summary>
    public interface IUsuarioActual
    {
        Guid UsuarioId { get; }
        string Email { get; }
        string Nombres { get; }

        /// <summary>
        /// Todos los claims del token actual, tal cual — usado para renovar
        /// el token (RenovarTokenCommand) sin tener que volver a consultar la BD.
        /// </summary>
        IEnumerable<Claim> ObtenerClaims();
    }
}
