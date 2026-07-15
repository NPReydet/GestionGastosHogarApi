using GGH.Application.Features.Usuarios.Dtos;
using MediatR;

namespace GGH.Application.Features.Usuarios.Commands.RenovarToken;

/// <summary>
/// No lleva parámetros: siempre opera sobre el usuario autenticado actual
/// (identificado por el JWT vigente en el request).
/// </summary>
public class RenovarTokenCommand : IRequest<TokenRespuestaDto>
{
}
