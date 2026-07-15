
namespace GGH.Domain.Exceptions
{
    public class GrupoFamiliarNoEncontradoException : Exception
    {
        public GrupoFamiliarNoEncontradoException()
            : base("No existe el grupo familiar indicado.") { }
    }
}
