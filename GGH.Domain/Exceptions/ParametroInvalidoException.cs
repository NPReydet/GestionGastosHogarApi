
namespace GGH.Domain.Exceptions
{
    /// <summary>
    /// Excepción genérica para validaciones simples de parámetros de entrada
    /// (mes fuera de rango, año inválido, límite <= 0, usuario sin grupo, etc.)
    /// que no ameritan una clase dedicada por cada caso.
    /// </summary>
    public class ParametroInvalidoException : Exception
    {
        public ParametroInvalidoException(string mensaje) : base(mensaje) { }
    }
}
