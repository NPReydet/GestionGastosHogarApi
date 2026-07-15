using GGH.Application.Common.Interfaces;

namespace GGH.Infrastructure.Auth
{
    public class ServicioHashContrasena : IServicioHashContrasena
    {
        public string Hashear(string contrasenaPlana) =>
            BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);

        public bool Verificar(string contrasenaPlana, string hashAlmacenado) =>
            BCrypt.Net.BCrypt.Verify(contrasenaPlana, hashAlmacenado);
    }
}
