
namespace GGH.Domain.Exceptions
{
    public class RutDuplicadoException : Exception
    {
        public RutDuplicadoException()
            : base("Ya existe una cuenta registrada con ese RUT.") { }
    }
}
