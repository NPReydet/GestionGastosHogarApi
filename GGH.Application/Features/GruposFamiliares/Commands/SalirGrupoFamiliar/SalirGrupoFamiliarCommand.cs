using GGH.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GGH.Application.Features.GruposFamiliares.Commands.SalirGrupoFamiliar
{
    public class SalirGrupoFamiliarHandler : IRequestHandler<SalirGrupoFamiliarCommand>
    {
        private readonly IRepositorioGruposFamiliares _repositorio;
        private readonly IUsuarioActual _usuarioActual;
        private readonly ILogger<SalirGrupoFamiliarHandler> _logger;

        public SalirGrupoFamiliarHandler(
            IRepositorioGruposFamiliares repositorio,
            IUsuarioActual usuarioActual,
            ILogger<SalirGrupoFamiliarHandler> logger)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
            _logger = logger;
        }

        public async Task Handle(SalirGrupoFamiliarCommand request, CancellationToken cancellationToken)
        {
            await _repositorio.SalirAsync(_usuarioActual.UsuarioId);

            _logger.LogInformation("Usuario {UsuarioId} salió de su grupo familiar", _usuarioActual.UsuarioId);
        }
    }
}
