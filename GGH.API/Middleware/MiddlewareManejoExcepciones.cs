using System.Net;
using System.Text.Json;
using FluentValidation;
using GGH.Domain.Exceptions;

namespace GGH.API.Middleware
{
    public class MiddlewareManejoExcepciones
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<MiddlewareManejoExcepciones> _logger;

        public MiddlewareManejoExcepciones(RequestDelegate siguiente, ILogger<MiddlewareManejoExcepciones> logger)
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _siguiente(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcionAsync(context, ex);
            }
        }

        private async Task ManejarExcepcionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (codigoEstado, mensaje) = ex switch
            {
                ErrorComunicacionBaseDatosException => (HttpStatusCode.ServiceUnavailable, ex.Message),
                CredencialesInvalidasException => (HttpStatusCode.Unauthorized, ex.Message),
                UsuarioNoEncontradoException => (HttpStatusCode.NotFound, ex.Message),
                GrupoFamiliarNoEncontradoException => (HttpStatusCode.NotFound, ex.Message),
                GastoNoEncontradoException => (HttpStatusCode.NotFound, ex.Message),
                IngresoNoEncontradoException => (HttpStatusCode.NotFound, ex.Message),
                AccesoNoAutorizadoException => (HttpStatusCode.Forbidden, ex.Message),
                RutDuplicadoException => (HttpStatusCode.Conflict, ex.Message),
                EmailDuplicadoException => (HttpStatusCode.Conflict, ex.Message),
                UsuarioYaEnGrupoException => (HttpStatusCode.Conflict, ex.Message),
                RutInvalidoException => (HttpStatusCode.BadRequest, ex.Message),
                CodigoGrupoInvalidoException => (HttpStatusCode.BadRequest, ex.Message),
                RangoFechasInvalidoException => (HttpStatusCode.BadRequest, ex.Message),
                MontoInvalidoException => (HttpStatusCode.BadRequest, ex.Message),
                CategoriaInvalidaException => (HttpStatusCode.BadRequest, ex.Message),
                ParametroInvalidoException => (HttpStatusCode.BadRequest, ex.Message),
                ValidationException validacion => (HttpStatusCode.BadRequest, string.Join(" | ", validacion.Errors.Select(e => e.ErrorMessage))),
                _ => (HttpStatusCode.InternalServerError, "Ocurrió un error inesperado. Por favor, intenta más tarde.")
            };
            if ((int)codigoEstado >= 500)
            {
                _logger.LogError(ex, "Error no controlado: {Mensaje}", ex.Message);
            }
            else
            {
                _logger.LogWarning("Excepción controlada: {Mensaje}", ex.Message);
            }

            context.Response.StatusCode = (int)codigoEstado;

            var respuesta = JsonSerializer.Serialize(new
            {
                mensaje,
                codigo = codigoEstado.ToString()
            });

            await context.Response.WriteAsync(respuesta);
        }
    }

}
