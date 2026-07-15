
namespace GGH.Application.Common.Interfaces
{
    public interface IServicioHashContrasena
    {
        bool Verificar(string contrasenaPlana, string hashAlmacenado);
        string Hashear(string contrasenaPlana);
    }
}
