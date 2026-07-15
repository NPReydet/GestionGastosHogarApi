using GGH.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GGH.Infrastructure.Security
{
    public class ServicioCifrado : IServicioCifrado
    {
        private readonly IConfiguration _configuracion;

        public ServicioCifrado(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        public string ObtenerLlaveCifrado()
        {
            return _configuracion["Cifrado:Llave"]
                ?? throw new InvalidOperationException("No se configuró 'Cifrado:Llave'.");
        }
    }
}
