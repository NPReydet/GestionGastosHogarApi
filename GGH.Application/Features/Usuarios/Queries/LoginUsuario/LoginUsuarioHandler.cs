using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Usuarios.Dtos;
using GGH.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GGH.Application.Features.Usuarios.Queries.LoginUsuario
{
    public class LoginUsuarioHandler : IRequestHandler<LoginUsuarioQuery, TokenRespuestaDto>
    {
        private readonly IRepositorioUsuarios _repositorioUsuarios;
        private readonly IServicioHashContrasena _servicioHash;
        private readonly IServicioToken _servicioToken;
        private readonly ILogger<LoginUsuarioHandler> _logger;

        public LoginUsuarioHandler(
            IRepositorioUsuarios repositorioUsuarios,
            IServicioHashContrasena servicioHash,
            IServicioToken servicioToken,
            ILogger<LoginUsuarioHandler> logger)
        {
            _repositorioUsuarios = repositorioUsuarios;
            _servicioHash = servicioHash;
            _servicioToken = servicioToken;
            _logger = logger;
        }

        public async Task<TokenRespuestaDto> Handle(LoginUsuarioQuery request, CancellationToken cancellationToken)
        {
            // Nota: si la base de datos no responde tras los reintentos de Polly,
            // IContextoDapper ya lanza ErrorComunicacionBaseDatosException, que
            // simplemente dejamos propagar; el middleware de la API la traduce a un 503.
            var usuario = await _repositorioUsuarios.ObtenerPorRutAsync(request.Rut, request.Dv);

            if (usuario is null || usuario.PasswordHash is null || !usuario.Vigente)
            {
                _logger.LogWarning("Intento de login fallido para rut {Rut}-{Dv}", request.Rut, request.Dv);
                throw new CredencialesInvalidasException();
            }

            var contrasenaValida = _servicioHash.Verificar(request.Contrasena, usuario.PasswordHash);

            if (!contrasenaValida)
            {
                _logger.LogWarning("Contraseña inválida para rut {Rut}-{Dv}", request.Rut, request.Dv);
                throw new CredencialesInvalidasException();
            }

            var token = _servicioToken.GenerarToken(usuario);

            return new TokenRespuestaDto
            {
                Token = token,
                Nombres = usuario.Nombres,
                Email = usuario.Email,
                ExpiraEn = DateTime.UtcNow.AddMinutes(30)
            };
        }
    }
}
