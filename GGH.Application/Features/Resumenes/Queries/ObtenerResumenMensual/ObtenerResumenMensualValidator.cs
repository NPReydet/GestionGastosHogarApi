using FluentValidation;

namespace GGH.Application.Features.Resumenes.Queries.ObtenerResumenMensual
{
    public class ObtenerResumenMensualValidator : AbstractValidator<ObtenerResumenMensualQuery>
    {
        public ObtenerResumenMensualValidator()
        {
            RuleFor(x => x.Mes).InclusiveBetween(1, 12).WithMessage("El mes debe estar entre 1 y 12.");
            RuleFor(x => x.Anio).InclusiveBetween(1900, 2100).WithMessage("El año ingresado no es válido.");
        }
    }
}
