
namespace GGH.Application.Features.Usuarios.Dtos
{
    public class UsuarioCreadoDto
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
