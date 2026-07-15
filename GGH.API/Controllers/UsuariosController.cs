using GGH.Application.Features.Usuarios.Commands.EliminarUsuarioCompleto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GGH.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Derecho al olvido: elimina por completo la cuenta del usuario
        /// autenticado (datos personales, gastos, ingresos). No lleva {id}
        /// en la ruta — siempre opera sobre el usuario del JWT, para que
        /// nadie pueda borrar la cuenta de otra persona.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> EliminarCuenta()
        {
            await _mediator.Send(new EliminarUsuarioCompletoCommand());
            return NoContent();
        }
    }
}
