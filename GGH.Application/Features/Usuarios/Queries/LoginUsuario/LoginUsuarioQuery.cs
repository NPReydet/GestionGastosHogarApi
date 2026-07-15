using GGH.Application.Features.Usuarios.Dtos;
using MediatR;

namespace GGH.Application.Features.Usuarios.Queries.LoginUsuario
{
    public class LoginUsuarioQuery : IRequest<TokenRespuestaDto>
    {
        public long Rut { get; set; }
        public char Dv { get; set; }
        public string Contrasena { get; set; } = default!;
    }
}
