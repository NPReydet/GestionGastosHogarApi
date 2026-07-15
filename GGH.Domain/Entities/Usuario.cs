using GGH.Domain.ValueObjects;

namespace GGH.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public long Rut { get; set; }
        public char Dv { get; set; }
        public string Nombres { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public string ApellidoMaterno { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PasswordHash { get; set; }
        public string? Auth0Id { get; set; }
        public bool Vigente { get; set; }
        public Guid? GrupoFamiliarId { get; set; }
        public Guid? UsuarioCreaId { get; set; }
        public Guid? UsuarioModificaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        public Rut ObtenerRut() => ValueObjects.Rut.Crear(Rut, Dv);
    }
}
