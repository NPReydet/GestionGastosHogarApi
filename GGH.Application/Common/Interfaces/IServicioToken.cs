using GGH.Domain.Entities;
using System.Security.Claims;

namespace GGH.Application.Common.Interfaces
{
    public interface IServicioToken
    {
        string GenerarToken(Usuario usuario);

        /// <summary>
        /// Reemite un JWT nuevo, con expiración fresca, a partir de los claims
        /// de un token todavía válido. No requiere volver a consultar la BD ni
        /// pedir credenciales de nuevo — solo copia la identidad ya verificada.
        /// </summary>
        string RenovarToken(IEnumerable<Claim> claimsActuales);
    }
}
