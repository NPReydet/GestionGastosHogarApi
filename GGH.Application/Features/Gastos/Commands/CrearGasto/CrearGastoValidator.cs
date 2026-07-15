using FluentValidation;

namespace GGH.Application.Features.Gastos.Commands.CrearGasto
{
    public class CrearGastoValidator : AbstractValidator<CrearGastoCommand>
    {
        private static readonly string[] MediosPagoValidos = { "Efectivo", "Debito", "Credito", "Transferencia" };

        public CrearGastoValidator()
        {
            RuleFor(x => x.CategoriaId).NotEmpty().WithMessage("La categoría es obligatoria.");
            RuleFor(x => x.Monto).GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es obligatoria.");
            RuleFor(x => x.MedioPago)
                .NotEmpty().WithMessage("El medio de pago es obligatorio.")
                .Must(m => MediosPagoValidos.Contains(m))
                .WithMessage($"El medio de pago debe ser uno de: {string.Join(", ", MediosPagoValidos)}.");
            RuleFor(x => x.Descripcion).MaximumLength(255);
            RuleFor(x => x.CuotasTotales).GreaterThan(0).When(x => x.CuotasTotales.HasValue)
                .WithMessage("Las cuotas totales deben ser mayores a 0.");
            RuleFor(x => x.CuotaActual)
                .GreaterThan(0).When(x => x.CuotaActual.HasValue)
                .LessThanOrEqualTo(x => x.CuotasTotales ?? int.MaxValue).When(x => x.CuotaActual.HasValue && x.CuotasTotales.HasValue)
                .WithMessage("La cuota actual no puede superar el total de cuotas.");
        }
    }
}
