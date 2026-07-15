using GGH.Application.Common.Interfaces;
using GGH.Application.Features.GruposFamiliares.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GGH.Application.Features.GruposFamiliares.Commands.CrearGrupoFamiliar
{
    public class CrearGrupoFamiliarHandler : IRequestHandler<CrearGrupoFamiliarCommand, GrupoFamiliarCreadoDto>
    {
        private readonly IRepositorioGruposFamiliares _repositorio;
        private readonly IUsuarioActual _usuarioActual;
        private readonly ILogger<CrearGrupoFamiliarHandler> _logger;

        public CrearGrupoFamiliarHandler(
            IRepositorioGruposFamiliares repositorio,
            IUsuarioActual usuarioActual,
            ILogger<CrearGrupoFamiliarHandler> logger)
        {
            _repositorio = repositorio;
            _usuarioActual = usuarioActual;
            _logger = logger;
        }

        public async Task<GrupoFamiliarCreadoDto> Handle(CrearGrupoFamiliarCommand request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.CrearAsync(_usuarioActual.UsuarioId, request.NombreGrupo);

            _logger.LogInformation(
                "Grupo familiar {GrupoId} creado por usuario {UsuarioId}, código {Codigo}",
                resultado.GrupoId, _usuarioActual.UsuarioId, resultado.Codigo);

            return resultado;
        }
    }
}
