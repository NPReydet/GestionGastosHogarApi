using GGH.Domain.Entities;
using System.Security.Claims;

namespace GGH.Application.Common.Interfaces
{
    public interface IServicioToken
    {
        string GenerarToken(Usuario usuario);

        string RenovarToken(IEnumerable<Claim> claimsActuales);
    }
}
