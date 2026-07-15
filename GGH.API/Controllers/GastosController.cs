using GGH.Application.Features.Gastos.Commands.ActualizarGasto;
using GGH.Application.Features.Gastos.Commands.CrearGasto;
using GGH.Application.Features.Gastos.Commands.EliminarGasto;
using GGH.Application.Features.Gastos.Queries.ListarCategoriasGasto;
using GGH.Application.Features.Gastos.Queries.ListarGastos;
using GGH.Application.Features.Gastos.Queries.ObtenerGastoPorId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GGH.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/gastos")]
    public class GastosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GastosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] DateOnly? desde, [FromQuery] DateOnly? hasta)
        {
            var resultado = await _mediator.Send(new ListarGastosQuery { Desde = desde, Hasta = hasta });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var resultado = await _mediator.Send(new ObtenerGastoPorIdQuery { GastoId = id });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearGastoCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarGastoCommand command)
        {
            command.GastoId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _mediator.Send(new EliminarGastoCommand { GastoId = id });
            return NoContent();
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> ListarCategorias()
        {
            var resultado = await _mediator.Send(new ListarCategoriasGastoQuery());
            return Ok(resultado);
        }
    }
}
