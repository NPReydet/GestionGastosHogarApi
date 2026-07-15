using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Commands.SalirGrupoFamiliar
{
    /// <summary>
    /// No lleva parámetros: siempre opera sobre el usuario autenticado actual,
    /// para evitar que alguien saque a otro usuario del grupo por error.
    /// </summary>
    public class SalirGrupoFamiliarCommand : IRequest
    {
    }
}
