
namespace GGH.Application.Features.GruposFamiliares.Dtos
{
    public class ResumenMensualFamiliarDto
    {
        public string Tipo { get; set; } = default!;      
        public string Categoria { get; set; } = default!;
        public Guid UsuarioId { get; set; }
        public string Nombres { get; set; } = default!;
        public decimal Total { get; set; }
        public long CantidadMovs { get; set; }
    }
}
