using FluentValidation;

namespace GGH.Application.Features.Resumenes.Queries.CompararPeriodos
{
    public class CompararPeriodosValidator : AbstractValidator<CompararPeriodosQuery>
    {
        public CompararPeriodosValidator()
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
