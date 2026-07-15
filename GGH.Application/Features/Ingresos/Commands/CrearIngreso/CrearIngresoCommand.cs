using MediatR;

namespace GGH.Application.Features.Ingresos.Commands.CrearIngreso
{
    public class CrearIngresoCommand : IRequest<Guid>
    {
        public Guid CategoriaId { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; }
    }
}
