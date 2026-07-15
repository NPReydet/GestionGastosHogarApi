
namespace GGH.Application.Features.Gastos.Dtos
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string? Descripcion { get; set; }
    }
}
