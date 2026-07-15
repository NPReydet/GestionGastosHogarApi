using System.Linq;
using System.Security.Claims;
using GGH.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GGH.Infrastructure.Auth;

public class UsuarioActual : IUsuarioActual
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioActual(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UsuarioId
    {
        get
        {
            var valor = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(valor) || !Guid.TryParse(valor, out var id))
            {
                throw new InvalidOperationException("No hay un usuario autenticado en el contexto actual.");
            }

            return id;
        }
    }

    public string Email
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                ?? throw new InvalidOperationException("No hay un usuario autenticado en el contexto actual.");
        }
    }

    public string Nombres
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value
                ?? throw new InvalidOperationException("No hay un usuario autenticado en el contexto actual.");
        }
    }

    public IEnumerable<Claim> ObtenerClaims()
    {
        return _httpContextAccessor.HttpContext?.User.Claims
            ?? throw new InvalidOperationException("No hay un usuario autenticado en el contexto actual.");
    }
}
