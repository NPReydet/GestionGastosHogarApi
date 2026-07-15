
namespace GGH.Domain.Exceptions
{
    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException()
            : base("No se encontró un usuario con esas credenciales.") { }
    }
}
