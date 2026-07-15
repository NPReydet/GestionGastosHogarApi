
namespace GGH.Application.Features.Usuarios.Dtos
{
    public class TokenRespuestaDto
    {
        public string Token { get; set; } = default!;
        public string Nombres { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime ExpiraEn { get; set; }
    }
}
