
namespace GGH.Application.Features.GruposFamiliares.Dtos
{
    public class MiembroGrupoDto
    {
        public Guid UsuarioId { get; set; }
        public string Nombres { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public bool EsCreador { get; set; }
        public DateTime? FechaUnion { get; set; }
    }

}
