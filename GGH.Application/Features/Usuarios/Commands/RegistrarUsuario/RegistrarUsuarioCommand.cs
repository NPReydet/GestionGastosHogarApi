using GGH.Application.Features.Usuarios.Dtos;
using MediatR;

namespace GGH.Application.Features.Usuarios.Commands.RegistrarUsuario
{
    public class RegistrarUsuarioCommand : IRequest<UsuarioCreadoDto>
    {
        public long Rut { get; set; }
        public char Dv { get; set; }
        public string Nombres { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public string ApellidoMaterno { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Contrasena { get; set; } = default!;
        public DateOnly FechaNacimiento { get; set; }
        public string Direccion { get; set; } = default!;
    }
}
