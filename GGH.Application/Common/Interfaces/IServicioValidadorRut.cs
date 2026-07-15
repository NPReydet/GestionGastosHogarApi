
namespace GGH.Application.Common.Interfaces
{
    public interface IServicioValidadorRut
    {
        /// <summary>
        /// Valida el dígito verificador en C# (algoritmo módulo 11), para
        /// fallar rápido antes de golpear la base de datos. La validación
        /// definitiva igual se repite en fn_validar_rut al insertar.
        /// </summary>
        bool EsValido(long rut, char dv);
    }
}
