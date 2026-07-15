using FluentValidation;

namespace GGH.Application.Features.GruposFamiliares.Queries.CompararPeriodosFamiliar
{
    public class CompararPeriodosFamiliarValidator : AbstractValidator<CompararPeriodosFamiliarQuery>
    {
        public CompararPeriodosFamiliarValidator()
        {
            RuleFor(x => x.Fin1)
                .GreaterThanOrEqualTo(x => x.Inicio1)
                .WithMessage("La fecha de fin del período 1 no puede ser anterior a la de inicio.");

            RuleFor(x => x.Fin2)
                .GreaterThanOrEqualTo(x => x.Inicio2)
                .WithMessage("La fecha de fin del período 2 no puede ser anterior a la de inicio.");
        }
    }
}
