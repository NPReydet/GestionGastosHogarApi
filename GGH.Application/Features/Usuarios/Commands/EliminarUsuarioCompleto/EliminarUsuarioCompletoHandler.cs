using GGH.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GGH.Application.Features.Usuarios.Commands.EliminarUsuarioCompleto
{
    public class EliminarUsuarioCompletoHandler : IRequestHandler<EliminarUsuarioCompletoCommand>
    {
        private readonly IRepositorioUsuarios _repositorio;
        private readonly IUsuarioActual _usuarioActual;
        private readonly ILogger<EliminarUsuarioCompletoHandler> _logger;

        public EliminarUsuarioCompletoHandler(
            IRepositorioUsuarios repositorio,
            IUsuarioActual usuarioActual,
            ILogger<EliminarUsuarioCompletoHandler> logger)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
            _logger = logger;
        }

        public async Task Handle(EliminarUsuarioCompletoCommand request, CancellationToken cancellationToken)
        {
            var usuarioId = _usuarioActual.UsuarioId;

            await _repositorio.EliminarCuentaAsync(usuarioId);

            _logger.LogInformation("Usuario {UsuarioId} eliminó su propia cuenta (derecho al olvido)", usuarioId);
        }
    }

}
