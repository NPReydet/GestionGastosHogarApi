
namespace GGH.Domain.Exceptions
{
    public class IngresoNoEncontradoException : Exception
    {
        public IngresoNoEncontradoException()
            : base("No existe el ingreso indicado.") { }
    }
}
