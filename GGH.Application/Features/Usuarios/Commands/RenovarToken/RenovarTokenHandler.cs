using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Usuarios.Dtos;
using MediatR;

namespace GGH.Application.Features.Usuarios.Commands.RenovarToken;

public class RenovarTokenHandler : IRequestHandler<RenovarTokenCommand, TokenRespuestaDto>
{
    private readonly IUsuarioActual _usuarioActual;
    private readonly IServicioToken _servicioToken;

    public RenovarTokenHandler(IUsuarioActual usuarioActual, IServicioToken servicioToken)
    {
        _usuarioActual = usuarioActual;
        _servicioToken = servicioToken;
    }

    public Task<TokenRespuestaDto> Handle(RenovarTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = _usuarioActual.ObtenerClaims();
        var tokenNuevo = _servicioToken.RenovarToken(claims);

        return Task.FromResult(new TokenRespuestaDto
        {
            Token = tokenNuevo,
            Nombres = _usuarioActual.Nombres,
            Email = _usuarioActual.Email,
            ExpiraEn = DateTime.UtcNow.AddMinutes(30)
        });
    }
}
