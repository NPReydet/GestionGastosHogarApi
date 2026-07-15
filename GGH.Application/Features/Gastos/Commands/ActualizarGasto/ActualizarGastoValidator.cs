using FluentValidation;
using MediatR;

namespace GGH.Application.Features.Gastos.Commands.ActualizarGasto
{
    public class ActualizarGastoValidator : AbstractValidator<ActualizarGastoCommand>
    {
        private static readonly string[] MediosPagoValidos = { "Efectivo", "Debito", "Credito", "Transferencia" };

        public ActualizarGastoValidator()
        {
            RuleFor(x => x.GastoId).NotEmpty().WithMessage("El id del gasto es obligatorio.");
            RuleFor(x => x.CategoriaId).NotEmpty().WithMessage("La categoría es obligatoria.");
            RuleFor(x => x.Monto).GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es obligatoria.");
            RuleFor(x => x.MedioPago)
                .NotEmpty().WithMessage("El medio de pago es obligatorio.")
                .Must(m => MediosPagoValidos.Contains(m))
                .WithMessage($"El medio de pago debe ser uno de: {string.Join(", ", MediosPagoValidos)}.");
            RuleFor(x => x.Descripcion).MaximumLength(255);
        }
    }
}
