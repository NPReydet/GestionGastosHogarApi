
namespace GGH.Application.Features.Ingresos.Dtos
{
    public class IngresoDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Categoria { get; set; } = default!;
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; }
    }
}
