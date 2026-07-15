using System.Text;
using GGH.Application.DependencyInjection;
using GGH.API.Middleware;
using GGH.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// --- Serilog: separado por nivel, tal como se definió en el diseño ---
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.File("logs/errores-.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
        rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/trace-.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// --- Capas de la solución ---
builder.Services.AgregarApplication();
builder.Services.AgregarInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Autenticación JWT: valida los tokens emitidos por ServicioToken en el login ---
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        var llaveJwt = builder.Configuration["Jwt:Llave"]
            ?? throw new InvalidOperationException("No se configuró 'Jwt:Llave'.");

        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Emisor"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audiencia"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llaveJwt)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    });
builder.Services.AddAuthorization();

// CORS para que GGH.Blazor (otro puerto/proceso) pueda consumir la API
builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("PermitirBlazor", politica =>
        politica.WithOrigins(builder.Configuration["UrlsPermitidas:Blazor"] ?? "https://localhost:7002")
                .AllowAnyHeader()
                .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// El middleware de excepciones va PRIMERO en el pipeline, para capturar
// absolutamente todo lo que ocurra después (incluidas fallas de conexión a BD).
app.UseMiddleware<MiddlewareManejoExcepciones>();

app.UseHttpsRedirection();
app.UseCors("PermitirBlazor");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Iniciando GGH.API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "GGH.API terminó inesperadamente durante el arranque");
}
finally
{
    Log.CloseAndFlush();
}
