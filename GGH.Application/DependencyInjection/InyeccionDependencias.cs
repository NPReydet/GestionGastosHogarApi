using FluentValidation;
using GGH.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GGH.Application.DependencyInjection
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarApplication(this IServiceCollection servicios)
        {
            var assembly = Assembly.GetExecutingAssembly();

            servicios.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            servicios.AddValidatorsFromAssembly(assembly);
            servicios.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportamientoValidacion<,>));

            return servicios;
        }
    }
}
