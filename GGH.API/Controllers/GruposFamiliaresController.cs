using GGH.Application.Features.GruposFamiliares.Commands.CrearGrupoFamiliar;
using GGH.Application.Features.GruposFamiliares.Commands.SalirGrupoFamiliar;
using GGH.Application.Features.GruposFamiliares.Commands.UnirseGrupoFamiliar;
using GGH.Application.Features.GruposFamiliares.Queries.CompararPeriodosFamiliar;
using GGH.Application.Features.GruposFamiliares.Queries.ListarMiembrosGrupoFamiliar;
using GGH.Application.Features.GruposFamiliares.Queries.ObtenerResumenAnualFamiliar;
using GGH.Application.Features.GruposFamiliares.Queries.ObtenerResumenMensualFamiliar;
using GGH.Application.Features.GruposFamiliares.Queries.ObtenerTopCategoriasGastoFamiliar;
using GGH.Application.Features.GruposFamiliares.Queries.ObtenerTopCategoriasIngresoFamiliar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GGH.API.Controllers
{
    /// <summary>
    /// Todos los endpoints requieren autenticación y siempre operan sobre el
    /// usuario autenticado actual (vía IUsuarioActual) — nunca reciben un
    /// usuarioId/grupoId por parámetro, para que un usuario no pueda
    /// consultar o manipular el grupo de otro.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/grupos-familiares")]
    public class GruposFamiliaresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GruposFamiliaresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] CrearGrupoFamiliarCommand command)
        {
            var resultado = await _mediator.Send(command);
            return CreatedAtAction(nameof(ListarMiembros), new { }, resultado);
        }

        [HttpPost("unirse")]
        public async Task<IActionResult> Unirse([FromBody] UnirseGrupoFamiliarCommand command)
        {
            var grupoId = await _mediator.Send(command);
            return Ok(new { grupoId });
        }

        [HttpPost("salir")]
        public async Task<IActionResult> Salir()
        {
            await _mediator.Send(new SalirGrupoFamiliarCommand());
            return NoContent();
        }

        [HttpGet("miembros")]
        public async Task<IActionResult> ListarMiembros()
        {
            var miembros = await _mediator.Send(new ListarMiembrosGrupoFamiliarQuery());
            return Ok(miembros);
        }

        [HttpGet("resumen-mensual")]
        public async Task<IActionResult> ResumenMensual([FromQuery] int mes, [FromQuery] int anio)
        {
            var resultado = await _mediator.Send(new ObtenerResumenMensualFamiliarQuery { Mes = mes, Anio = anio });
            return Ok(resultado);
        }

        [HttpGet("resumen-anual")]
        public async Task<IActionResult> ResumenAnual([FromQuery] int anio)
        {
            var resultado = await _mediator.Send(new ObtenerResumenAnualFamiliarQuery { Anio = anio });
            return Ok(resultado);
        }

        [HttpGet("comparar")]
        public async Task<IActionResult> Comparar(
            [FromQuery] DateOnly inicio1, [FromQuery] DateOnly fin1,
            [FromQuery] DateOnly inicio2, [FromQuery] DateOnly fin2)
        {
            var resultado = await _mediator.Send(new CompararPeriodosFamiliarQuery
            {
                Inicio1 = inicio1,
                Fin1 = fin1,
                Inicio2 = inicio2,
                Fin2 = fin2
            });
            return Ok(resultado);
        }

        [HttpGet("top-gastos")]
        public async Task<IActionResult> TopGastos([FromQuery] DateOnly desde, [FromQuery] DateOnly hasta, [FromQuery] int limite = 5)
        {
            var resultado = await _mediator.Send(new ObtenerTopCategoriasGastoFamiliarQuery { Desde = desde, Hasta = hasta, Limite = limite });
            return Ok(resultado);
        }

        [HttpGet("top-ingresos")]
        public async Task<IActionResult> TopIngresos([FromQuery] DateOnly desde, [FromQuery] DateOnly hasta, [FromQuery] int limite = 5)
        {
            var resultado = await _mediator.Send(new ObtenerTopCategoriasIngresoFamiliarQuery { Desde = desde, Hasta = hasta, Limite = limite });
            return Ok(resultado);
        }
    }
}
