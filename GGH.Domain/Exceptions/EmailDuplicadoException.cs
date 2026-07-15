
namespace GGH.Domain.Exceptions
{
    public class EmailDuplicadoException : Exception
    {
        public EmailDuplicadoException()
            : base("Ya existe una cuenta registrada con ese email.") { }
    }
}
