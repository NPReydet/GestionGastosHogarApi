
namespace GGH.Application.Features.Resumenes.Dtos
{
    public class ResumenMensualDto
    {
        public string Tipo { get; set; } = default!;       // "Gasto" | "Ingreso"
        public string Categoria { get; set; } = default!;
        public decimal Total { get; set; }
        public long CantidadMovs { get; set; }
    }
}
