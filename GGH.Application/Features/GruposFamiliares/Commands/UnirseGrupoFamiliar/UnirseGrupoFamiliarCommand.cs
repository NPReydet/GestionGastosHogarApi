using MediatR;

namespace GGH.Application.Features.GruposFamiliares.Commands.UnirseGrupoFamiliar
{
    public class UnirseGrupoFamiliarCommand : IRequest<Guid>
    {
        public string Codigo { get; set; } = default!;
    }
}
