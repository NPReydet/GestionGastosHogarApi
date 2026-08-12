
namespace GGH.Domain.Exceptions
{
    public class ErrorComunicacionBaseDatosException : Exception
    {
        public ErrorComunicacionBaseDatosException()
            : base("Actualmente hay problemas de comunicación con el servidor. Por favor, intenta más tarde.") { }
    }
}
