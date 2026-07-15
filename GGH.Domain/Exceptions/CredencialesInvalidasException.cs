
namespace GGH.Domain.Exceptions
{
    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException()
            : base("El rut o la contraseña son incorrectos.") { }
    }
}
