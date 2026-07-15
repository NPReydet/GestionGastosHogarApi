
namespace GGH.Application.Features.Resumenes.Dtos
{
    public class TopCategoriaDto
    {
        public string Categoria { get; set; } = default!;
        public decimal Total { get; set; }
        public long CantidadMovs { get; set; }
    }
}
