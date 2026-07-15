using FluentValidation;

namespace GGH.Application.Features.GruposFamiliares.Commands.CrearGrupoFamiliar
{
    public class CrearGrupoFamiliarValidator : AbstractValidator<CrearGrupoFamiliarCommand>
    {
        public CrearGrupoFamiliarValidator()
        {
            RuleFor(x => x.NombreGrupo)
                .MaximumLength(100).WithMessage("El nombre del grupo no puede superar los 100 caracteres.");
        }
    }

}
