
namespace GGH.Application.Features.Resumenes.Dtos
{
    public class ResumenAnualDto
    {
        public int Mes { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Balance { get; set; }
    }
}
