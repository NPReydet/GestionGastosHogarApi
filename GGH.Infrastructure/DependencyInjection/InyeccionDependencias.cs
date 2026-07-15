using Dapper;
using GGH.Application.Common.Interfaces;
using GGH.Infrastructure.Auth;
using GGH.Infrastructure.Persistence;
using GGH.Infrastructure.Repositories;
using GGH.Infrastructure.Security;
using GGH.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace GGH.Infrastructure.DependencyInjection
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarInfrastructure(this IServiceCollection servicios)
        {
            // Dapper por defecto mapea 1 a 1 los nombres de columna; nuestras
            // columnas están en snake_case (fecha_creacion) y las propiedades
            // en PascalCase (FechaCreacion), así que activamos el mapeo automático.
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            servicios.AddSingleton<IContextoDapper, ContextoDapper>();
            servicios.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            servicios.AddScoped<IRepositorioGruposFamiliares, RepositorioGruposFamiliares>();
            servicios.AddScoped<IRepositorioGastos, RepositorioGastos>();
            servicios.AddScoped<IRepositorioIngresos, RepositorioIngresos>();
            servicios.AddScoped<IRepositorioResumenes, RepositorioResumenes>();
            servicios.AddScoped<IServicioHashContrasena, ServicioHashContrasena>();
            servicios.AddScoped<IServicioToken, ServicioToken>();
            servicios.AddScoped<IServicioValidadorRut, ServicioValidadorRut>();
            servicios.AddScoped<IServicioCifrado, ServicioCifrado>();
            servicios.AddScoped<IUsuarioActual, UsuarioActual>();

            return servicios;
        }
    }
}
