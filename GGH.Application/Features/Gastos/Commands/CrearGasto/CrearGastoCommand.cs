using MediatR;

namespace GGH.Application.Features.Gastos.Commands.CrearGasto
{
    public class CrearGastoCommand : IRequest<Guid>
    {
        public Guid CategoriaId { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string MedioPago { get; set; } = default!;
        public bool Recurrente { get; set; }
        public int? CuotasTotales { get; set; }
        public int? CuotaActual { get; set; }
    }
}
