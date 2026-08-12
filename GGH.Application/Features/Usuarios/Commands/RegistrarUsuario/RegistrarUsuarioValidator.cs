using FluentValidation;
using GGH.Application.Common.Interfaces;

namespace GGH.Application.Features.Usuarios.Commands.RegistrarUsuario
{
    public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioCommand>
    {
        public RegistrarUsuarioValidator(IServicioValidadorRut validadorRut)
        {
            RuleFor(x => x.Rut)
                .GreaterThan(0).WithMessage("El rut es obligatorio.");

            RuleFor(x => x)
                .Must(x => validadorRut.EsValido(x.Rut, x.Dv))
                .WithMessage("El rut ingresado no es válido.")
                .WithName("Rut");

            RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("Los nombres son obligatorios.")
                .MaximumLength(100);

            RuleFor(x => x.ApellidoPaterno)
                .NotEmpty().WithMessage("El apellido paterno es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.ApellidoMaterno)
                .NotEmpty().WithMessage("El apellido materno es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.");

            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("La fecha de nacimiento debe ser en el pasado.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(255);
        }
    }
}
