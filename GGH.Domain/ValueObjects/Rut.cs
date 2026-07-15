using GGH.Domain.Exceptions;

namespace GGH.Domain.ValueObjects
{
    /// <summary>
    /// Representa un RUT chileno inmutable, ya validado.
    /// La validación del dígito verificador se hace en la base de datos
    /// (fn_validar_rut) al momento de registrar; este VO solo garantiza
    /// el formato correcto en memoria.
    /// </summary>
    public sealed class Rut
    {
        public long Numero { get; }
        public char Dv { get; }

        private Rut(long numero, char dv)
        {
            Numero = numero;
            Dv = char.ToUpperInvariant(dv);
        }

        public static Rut Crear(long numero, char dv)
        {
            if (numero <= 0)
            {
                throw new RutInvalidoException("El número de RUT debe ser mayor a 0.");
            }

            var dvNormalizado = char.ToUpperInvariant(dv);
            var dvValido = char.IsDigit(dvNormalizado) || dvNormalizado == 'K';

            if (!dvValido)
            {
                throw new RutInvalidoException("El dígito verificador debe ser un número o la letra K.");
            }

            return new Rut(numero, dvNormalizado);
        }

        public override string ToString() => $"{Numero}-{Dv}";
    }
}
