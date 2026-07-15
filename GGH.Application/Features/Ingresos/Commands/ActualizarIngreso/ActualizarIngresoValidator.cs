using FluentValidation;

namespace GGH.Application.Features.Ingresos.Commands.ActualizarIngreso
{
    public class ActualizarIngresoValidator : AbstractValidator<ActualizarIngresoCommand>
    {
        public ActualizarIngresoValidator()
        {
            RuleFor(x => x.IngresoId).NotEmpty().WithMessage("El id del ingreso es obligatorio.");
            RuleFor(x => x.CategoriaId).NotEmpty().WithMessage("La categoría es obligatoria.");
            RuleFor(x => x.Monto).GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es obligatoria.");
            RuleFor(x => x.Descripcion).MaximumLength(255);
        }
    }
}
