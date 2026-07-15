using FluentValidation;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerResumenAnual
{
    public class ObtenerResumenAnualValidator : AbstractValidator<ObtenerResumenAnualQuery>
    {
        public ObtenerResumenAnualValidator()
        {
            RuleFor(x => x.Anio).InclusiveBetween(1900, 2100).WithMessage("El año ingresado no es válido.");
        }
    }

}
