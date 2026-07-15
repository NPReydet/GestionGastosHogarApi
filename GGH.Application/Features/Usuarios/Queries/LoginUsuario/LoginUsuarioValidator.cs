using FluentValidation;

namespace GGH.Application.Features.Usuarios.Queries.LoginUsuario
{
    public class LoginUsuarioValidator : AbstractValidator<LoginUsuarioQuery>
    {
        public LoginUsuarioValidator()
        {
            RuleFor(x => x.Rut).GreaterThan(0)
                .WithMessage("El rut es obligatorio.");

            RuleFor(x => x.Dv).NotEmpty()
                .WithMessage("El dígito verificador es obligatorio.");

            RuleFor(x => x.Contrasena).NotEmpty()
                .WithMessage("La contraseña es obligatoria.");
        }
    }
}
