using System;
using System.Collections.Generic;
using System.Text;

namespace GGH.Application.Common.Interfaces
{
    /// <summary>
    /// El cifrado/descifrado real de datos sensibles (fecha de nacimiento,
    /// dirección) ocurre dentro de PostgreSQL vía pgcrypto (fn_cifrar_dato /
    /// fn_descifrar_dato). Este servicio solo entrega la llave configurada
    /// de forma segura, para no esparcir IConfiguration por los repositorios.
    /// </summary>
    public interface IServicioCifrado
    {
        string ObtenerLlaveCifrado();
    }
}
