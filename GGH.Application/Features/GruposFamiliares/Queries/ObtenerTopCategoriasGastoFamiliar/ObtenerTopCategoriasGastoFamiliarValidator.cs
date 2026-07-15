using FluentValidation;

namespace GGH.Application.Features.GruposFamiliares.Queries.ObtenerTopCategoriasGastoFamiliar
{
    public class ObtenerTopCategoriasGastoFamiliarValidator : AbstractValidator<ObtenerTopCategoriasGastoFamiliarQuery>
    {
        public ObtenerTopCategoriasGastoFamiliarValidator()
        {
            RuleFor(x => x.Hasta)
                .GreaterThanOrEqualTo(x => x.Desde)
                .WithMessage("La fecha 'hasta' no puede ser anterior a la fecha 'desde'.");

            RuleFor(x => x.Limite)
                .GreaterThan(0).WithMessage("El límite de resultados debe ser mayor a 0.");
        }
    }
}
