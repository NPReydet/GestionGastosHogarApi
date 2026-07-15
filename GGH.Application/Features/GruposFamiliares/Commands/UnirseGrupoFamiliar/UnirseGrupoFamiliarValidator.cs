using FluentValidation;

namespace GGH.Application.Features.GruposFamiliares.Commands.UnirseGrupoFamiliar
{
    public class UnirseGrupoFamiliarValidator : AbstractValidator<UnirseGrupoFamiliarCommand>
    {
        public UnirseGrupoFamiliarValidator()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código del grupo familiar es obligatorio.")
                .MaximumLength(8);
        }
    }
}
