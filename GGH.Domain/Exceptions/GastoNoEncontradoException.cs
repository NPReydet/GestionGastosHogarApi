
namespace GGH.Domain.Exceptions
{
    public class GastoNoEncontradoException : Exception
    {
        public GastoNoEncontradoException()
            : base("No existe el gasto indicado.") { }
    }
}
