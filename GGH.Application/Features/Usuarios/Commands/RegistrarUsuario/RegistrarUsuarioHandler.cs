using GGH.Application.Common.Interfaces;
using GGH.Application.Features.Usuarios.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GGH.Application.Features.Usuarios.Commands.RegistrarUsuario
{
    public class RegistrarUsuarioHandler : IRequestHandler<RegistrarUsuarioCommand, UsuarioCreadoDto>
    {
        private readonly IRepositorioUsuarios _repositorioUsuarios;
        private readonly IServicioHashContrasena _servicioHash;
        private readonly ILogger<RegistrarUsuarioHandler> _logger;

        public RegistrarUsuarioHandler(
            IRepositorioUsuarios repositorioUsuarios,
            IServicioHashContrasena servicioHash,
            ILogger<RegistrarUsuarioHandler> logger)
        {
            _repositorioUsuarios = repositorioUsuarios;
            _servicioHash = servicioHash;
            _logger = logger;
        }

        public async Task<UsuarioCreadoDto> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _servicioHash.Hashear(request.Contrasena);

            // Nota: si el rut o el email ya existen, el repositorio traduce el
            // error de PostgreSQL (RAISE EXCEPTION) a RutDuplicadoException o
            // EmailDuplicadoException, que el middleware convierte en un 409.
            var usuarioId = await _repositorioUsuarios.CrearAsync(
                request.Rut,
                request.Dv,
                request.Nombres,
                request.ApellidoPaterno,
                request.ApellidoMaterno,
                request.Email,
                passwordHash,
                request.FechaNacimiento,
                request.Direccion);

            _logger.LogInformation("Usuario registrado exitosamente: {UsuarioId}", usuarioId);

            return new UsuarioCreadoDto
            {
                Id = usuarioId,
                Nombres = request.Nombres,
                Email = request.Email
            };
        }
    }
}
