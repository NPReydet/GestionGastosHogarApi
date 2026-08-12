
namespace GGH.Domain.Exceptions
{
    public class AccesoNoAutorizadoException : Exception
    {
        public AccesoNoAutorizadoException()
            : base("No tienes permiso para acceder a este recurso.") { }
    }
}
