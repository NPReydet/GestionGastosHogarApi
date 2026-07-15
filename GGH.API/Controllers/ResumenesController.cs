using GGH.Application.Features.Resumenes.Queries.CompararPeriodos;
using GGH.Application.Features.Resumenes.Queries.ObtenerResumenAnual;
using GGH.Application.Features.Resumenes.Queries.ObtenerResumenMensual;
using GGH.Application.Features.Resumenes.Queries.ObtenerTopCategoriasGasto;
using GGH.Application.Features.Resumenes.Queries.ObtenerTopCategoriasIngreso;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GGH.API.Controllers
{
    /// <summary>
    /// Resúmenes sobre los propios gastos/ingresos del usuario autenticado,
    /// sin considerar grupo familiar (equivalente individual de
    /// GruposFamiliaresController). Útil tanto para usuarios que no
    /// pertenecen a ningún grupo como para que un miembro de un grupo
    /// vea "solo lo mío".
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/resumenes")]
    public class ResumenesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResumenesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("mensual")]
        public async Task<IActionResult> ResumenMensual([FromQuery] int mes, [FromQuery] int anio)
        {
            var resultado = await _mediator.Send(new ObtenerResumenMensualQuery { Mes = mes, Anio = anio });
            return Ok(resultado);
        }

        [HttpGet("anual")]
        public async Task<IActionResult> ResumenAnual([FromQuery] int anio)
        {
            var resultado = await _mediator.Send(new ObtenerResumenAnualQuery { Anio = anio });
            return Ok(resultado);
        }

        [HttpGet("comparar")]
        public async Task<IActionResult> Comparar(
            [FromQuery] DateOnly inicio1, [FromQuery] DateOnly fin1,
            [FromQuery] DateOnly inicio2, [FromQuery] DateOnly fin2)
        {
            var resultado = await _mediator.Send(new CompararPeriodosQuery
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
            var resultado = await _mediator.Send(new ObtenerTopCategoriasGastoQuery { Desde = desde, Hasta = hasta, Limite = limite });
            return Ok(resultado);
        }

        [HttpGet("top-ingresos")]
        public async Task<IActionResult> TopIngresos([FromQuery] DateOnly desde, [FromQuery] DateOnly hasta, [FromQuery] int limite = 5)
        {
            var resultado = await _mediator.Send(new ObtenerTopCategoriasIngresoQuery { Desde = desde, Hasta = hasta, Limite = limite });
            return Ok(resultado);
        }
    }

}
