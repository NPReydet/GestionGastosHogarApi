using GGH.Application.Features.Ingresos.Commands.ActualizarIngreso;
using GGH.Application.Features.Ingresos.Commands.CrearIngreso;
using GGH.Application.Features.Ingresos.Commands.EliminarIngreso;
using GGH.Application.Features.Ingresos.Queries.ListarCategoriasIngreso;
using GGH.Application.Features.Ingresos.Queries.ListarIngresos;
using GGH.Application.Features.Ingresos.Queries.ObtenerIngresoPorId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GGH.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/ingresos")]
    public class IngresosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngresosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] DateOnly? desde, [FromQuery] DateOnly? hasta)
        {
            var resultado = await _mediator.Send(new ListarIngresosQuery { Desde = desde, Hasta = hasta });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var resultado = await _mediator.Send(new ObtenerIngresoPorIdQuery { IngresoId = id });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearIngresoCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarIngresoCommand command)
        {
            command.IngresoId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _mediator.Send(new EliminarIngresoCommand { IngresoId = id });
            return NoContent();
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> ListarCategorias()
        {
            var resultado = await _mediator.Send(new ListarCategoriasIngresoQuery());
            return Ok(resultado);
        }
    }
}
