using MediatR;

namespace GGH.Application.Features.Usuarios.Commands.EliminarUsuarioCompleto
{
    /// <summary>
    /// No lleva parámetros: siempre opera sobre el usuario autenticado actual,
    /// para que nadie pueda eliminar la cuenta de otra persona.
    /// </summary>
    public class EliminarUsuarioCompletoCommand : IRequest
    {
    }
}
