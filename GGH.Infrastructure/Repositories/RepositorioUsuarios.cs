using GGH.Application.Common.Interfaces;
using GGH.Domain.Entities;
using GGH.Domain.Exceptions;
using Npgsql;

namespace GGH.Infrastructure.Repositories
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly IContextoDapper _contexto;
        private readonly IServicioCifrado _servicioCifrado;

        public RepositorioUsuarios(IContextoDapper contexto, IServicioCifrado servicioCifrado)
        {
            _contexto = contexto;
            _servicioCifrado = servicioCifrado;
        }

        public async Task<Usuario?> ObtenerPorRutAsync(long rut, char dv)
        {
            return await _contexto.EjecutarQuerySingleAsync<Usuario>(
                "sp_obtener_usuario_por_rut",
                new { p_rut = rut, p_dv = dv.ToString() });
        }

        public async Task<Usuario?> ObtenerPorAuth0IdAsync(string auth0Id)
        {
            return await _contexto.EjecutarQuerySingleAsync<Usuario>(
                "sp_obtener_usuario_por_auth0_id",
                new { p_auth0_id = auth0Id });
        }

        public async Task<Guid> CrearAsync(
            long rut,
            char dv,
            string nombres,
            string apellidoPaterno,
            string apellidoMaterno,
            string email,
            string passwordHash,
            DateOnly fechaNacimiento,
            string direccion)
        {
            try
            {
                return await _contexto.EjecutarEscalarAsync<Guid>(
                    "sp_registrar_usuario",
                    new
                    {
                        p_rut = rut,
                        p_dv = dv.ToString(),
                        p_nombres = nombres,
                        p_apellido_paterno = apellidoPaterno,
                        p_apellido_materno = apellidoMaterno,
                        p_email = email,
                        p_password_hash = passwordHash,
                        p_auth0_id = (string?)null,
                        // Se envía como texto ISO 'yyyy-MM-dd' y se castea a DATE
                        // dentro de la función SQL — evita el problema de Npgsql
                        // enviando DateOnly/DateTime con un tipo distinto al esperado.
                        p_fecha_nacimiento = fechaNacimiento.ToString("yyyy-MM-dd"),
                        p_direccion = direccion,
                        p_llave_cifrado = _servicioCifrado.ObtenerLlaveCifrado()
                    });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("RUT_INVALIDO"))
            {
                throw new RutInvalidoException("El rut ingresado no es válido.");
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("RUT_DUPLICADO"))
            {
                throw new RutDuplicadoException();
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("EMAIL_DUPLICADO"))
            {
                throw new EmailDuplicadoException();
            }
        }

        public async Task EliminarCuentaAsync(Guid usuarioId)
        {
            try
            {
                await _contexto.EjecutarAsync(
                    "sp_eliminar_usuario_completo",
                    new { p_usuario_id = usuarioId });
            }
            catch (PostgresException ex) when (ex.MessageText.StartsWith("USUARIO_NO_ENCONTRADO"))
            {
                throw new UsuarioNoEncontradoException();
            }
        }
    }

}
