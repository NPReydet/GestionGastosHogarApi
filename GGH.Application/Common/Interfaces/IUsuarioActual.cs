
using System.Security.Claims;

namespace GGH.Application.Common.Interfaces
{
    public interface IUsuarioActual
    {
        Guid UsuarioId { get; }
        string Email { get; }
        string Nombres { get; }

        IEnumerable<Claim> ObtenerClaims();
    }
}
