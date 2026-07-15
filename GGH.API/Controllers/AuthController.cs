using GGH.Application.Features.Usuarios.Commands.RegistrarUsuario;
using GGH.Application.Features.Usuarios.Commands.RenovarToken;
using GGH.Application.Features.Usuarios.Queries.LoginUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GGH.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Login por RUT chileno + contraseña.
    /// Nota: si la BD no responde, el middleware global devuelve un 503
    /// con mensaje amigable en vez de que la petición explote sin control.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioQuery query)
    {
        var resultado = await _mediator.Send(query);
        return Ok(resultado);
    }

    /// <summary>
    /// Registro de una nueva cuenta. Cifra fecha de nacimiento y dirección
    /// a nivel de BD (pgcrypto) y devuelve el id del usuario creado.
    /// </summary>
    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] RegistrarUsuarioCommand command)
    {
        var resultado = await _mediator.Send(command);
        return CreatedAtAction(nameof(Login), new { }, resultado);
    }

    /// <summary>
    /// Extiende la sesión emitiendo un token nuevo con expiración fresca,
    /// a partir de un token todavía válido (requiere estar autenticado).
    /// El frontend la llama cuando el usuario confirma seguir conectado
    /// tras el aviso de expiración próxima.
    /// </summary>
    [Authorize]
    [HttpPost("renovar")]
    public async Task<IActionResult> Renovar()
    {
        var resultado = await _mediator.Send(new RenovarTokenCommand());
        return Ok(resultado);
    }
}
