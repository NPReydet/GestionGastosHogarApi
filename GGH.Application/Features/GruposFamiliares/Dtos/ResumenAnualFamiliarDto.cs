
namespace GGH.Application.Features.GruposFamiliares.Dtos
{
    public class ResumenAnualFamiliarDto
    {
        public int Mes { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Balance { get; set; }
    }
}
