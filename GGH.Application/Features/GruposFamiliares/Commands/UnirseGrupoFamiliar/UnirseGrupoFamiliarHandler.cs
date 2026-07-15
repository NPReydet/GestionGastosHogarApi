using GGH.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GGH.Application.Features.GruposFamiliares.Commands.UnirseGrupoFamiliar
{
    public class UnirseGrupoFamiliarHandler : IRequestHandler<UnirseGrupoFamiliarCommand, Guid>
    {
        private readonly IRepositorioGruposFamiliares _repositorio;
        private readonly IUsuarioActual _usuarioActual;
        private readonly ILogger<UnirseGrupoFamiliarHandler> _logger;

        public UnirseGrupoFamiliarHandler(
            IRepositorioGruposFamiliares repositorio,
            IUsuarioActual usuarioActual,
            ILogger<UnirseGrupoFamiliarHandler> logger)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
            _logger = logger;
        }

        public async Task<Guid> Handle(UnirseGrupoFamiliarCommand request, CancellationToken cancellationToken)
        {
            var grupoId = await _repositorio.UnirseAsync(_usuarioActual.UsuarioId, request.Codigo);

            _logger.LogInformation(
                "Usuario {UsuarioId} se unió al grupo familiar {GrupoId}", _usuarioActual.UsuarioId, grupoId);

            return grupoId;
        }
    }
}
