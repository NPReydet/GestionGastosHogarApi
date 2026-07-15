
namespace GGH.Application.Features.Gastos.Dtos
{
    public class GastoDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Categoria { get; set; } = default!;
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string MedioPago { get; set; } = default!;
        public bool Recurrente { get; set; }
        public int? CuotasTotales { get; set; }
        public int? CuotaActual { get; set; }
    }
}
