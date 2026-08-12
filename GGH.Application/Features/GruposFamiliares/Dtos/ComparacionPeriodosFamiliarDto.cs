
namespace GGH.Application.Features.GruposFamiliares.Dtos
{
    public class ComparacionPeriodosFamiliarDto
    {
        public string Periodo { get; set; } = default!;   
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Balance { get; set; }
    }
}
