
namespace GGH.Application.Features.Resumenes.Dtos
{
    public class ComparacionPeriodosDto
    {
        public string Periodo { get; set; } = default!;   // "Periodo 1" | "Periodo 2"
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Balance { get; set; }
    }
}
