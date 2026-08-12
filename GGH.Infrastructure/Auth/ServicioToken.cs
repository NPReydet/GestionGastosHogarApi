using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GGH.Application.Common.Interfaces;
using GGH.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GGH.Infrastructure.Auth;

public class ServicioToken : IServicioToken
{
    private readonly IConfiguration _configuracion;

    public ServicioToken(IConfiguration configuracion)
    {
        _configuracion = configuracion;
    }

    public string GenerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim("rut", $"{usuario.Rut}-{usuario.Dv}"),
            new Claim(ClaimTypes.GivenName, usuario.Nombres)
        };

        return FirmarToken(claims);
    }

    public string RenovarToken(IEnumerable<Claim> claimsActuales)
    {
        // Se reutilizan los mismos claims del token vigente (identidad ya
        // verificada en el login original), solo se reemite con una nueva
        // fecha de expiración. No se vuelve a consultar la base de datos.
        var claimsRelevantes = claimsActuales
            .Where(c => c.Type is ClaimTypes.NameIdentifier or ClaimTypes.Email or "rut" or ClaimTypes.GivenName)
            .Select(c => new Claim(c.Type, c.Value));

        return FirmarToken(claimsRelevantes);
    }

    private string FirmarToken(IEnumerable<Claim> claims)
    {
        var llave = _configuracion["Jwt:Llave"]
            ?? throw new InvalidOperationException("No se configuró 'Jwt:Llave'.");

        var credenciales = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llave)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuracion["Jwt:Emisor"],
            audience: _configuracion["Jwt:Audiencia"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
