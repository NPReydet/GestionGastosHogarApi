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

     
        [HttpDelete]
        public async Task<IActionResult> EliminarCuenta()
        {
            await _mediator.Send(new EliminarUsuarioCompletoCommand());
            return NoContent();
        }
    }
}
