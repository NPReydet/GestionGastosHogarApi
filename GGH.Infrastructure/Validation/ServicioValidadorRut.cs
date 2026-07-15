using GGH.Application.Common.Interfaces;

namespace GGH.Infrastructure.Validation
{
    public class ServicioValidadorRut : IServicioValidadorRut
    {
        public bool EsValido(long rut, char dv)
        {
            if (rut <= 0)
            {
                return false;
            }

            var suma = 0;
            var multiplo = 2;
            var rutTemp = rut;

            while (rutTemp > 0)
            {
                suma += (int)(rutTemp % 10) * multiplo;
                rutTemp /= 10;
                multiplo = multiplo == 7 ? 2 : multiplo + 1;
            }

            var resto = 11 - (suma % 11);

            var dvEsperado = resto switch
            {
                11 => '0',
                10 => 'K',
                _ => (char)('0' + resto)
            };

            return char.ToUpperInvariant(dvEsperado) == char.ToUpperInvariant(dv);
        }
    }
}
