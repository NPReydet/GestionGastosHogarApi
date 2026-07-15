
namespace GGH.Domain.Exceptions
{
    /// <summary>
    /// Se lanza cuando un usuario intenta acceder, modificar o eliminar
    /// un recurso (gasto, ingreso, etc.) que pertenece a otro usuario.
    /// </summary>
    public class AccesoNoAutorizadoException : Exception
    {
        public AccesoNoAutorizadoException()
            : base("No tienes permiso para acceder a este recurso.") { }
    }
}
