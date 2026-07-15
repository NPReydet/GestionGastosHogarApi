using FluentValidation;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerResumenAnualFamiliar
{
    public class ObtenerResumenAnualFamiliarValidator : AbstractValidator<ObtenerResumenAnualFamiliarQuery>
    {
        public ObtenerResumenAnualFamiliarValidator()
        {
            RuleFor(x => x.Anio).InclusiveBetween(1900, 2100).WithMessage("El año ingresado no es válido.");
        }
    }
}
