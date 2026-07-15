
namespace GGH.Domain.Exceptions
{
    /// <summary>
    /// Se lanza cuando, tras agotar los reintentos de conexión (Polly),
    /// la base de datos sigue sin responder.
    /// </summary>
    public class ErrorComunicacionBaseDatosException : Exception
    {
        public ErrorComunicacionBaseDatosException()
            : base("Actualmente hay problemas de comunicación con el servidor. Por favor, intenta más tarde.") { }
    }
}
