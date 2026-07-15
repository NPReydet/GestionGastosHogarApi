# Gestor de Gastos del Hogar — API

Backend de la aplicación de gestión de gastos e ingresos familiares. Expone una API REST construida con .NET, siguiendo Clean Architecture y CQRS, con toda la lógica de negocio implementada en funciones de PostgreSQL.

> El frontend (React) vive en un repositorio separado: [ggh.app](#) *(agrega aquí el link a tu otro repo cuando lo subas)*

## Características

- Autenticación por RUT chileno + contraseña, JWT con renovación de sesión
- Gastos e Ingresos: CRUD completo con categorías, cuotas, filtros por fecha
- Grupos Familiares: creación por código de invitación, resúmenes agregados por integrante
- Resúmenes financieros: mensual, anual, comparación de periodos, top categorías
- Datos sensibles cifrados a nivel de columna (pgcrypto), derecho al olvido, contraseñas con BCrypt
- Reintentos automáticos ante fallas transitorias de conexión a BD (Polly)

## Stack tecnológico

- .NET 10 (C#) — Clean Architecture (`Domain` / `Application` / `Infrastructure` / `API`)
- CQRS con MediatR, validación con FluentValidation
- PostgreSQL — lógica de negocio en funciones SQL, acceso con Dapper (sin ORM)
- JWT Bearer, BCrypt, Polly, Serilog
- Docker

## Arquitectura

```
GGH.Domain          → Entidades, excepciones y value objects. Sin dependencias externas.
GGH.Application      → Casos de uso (CQRS: Commands/Queries + Handlers), interfaces, DTOs.
GGH.Infrastructure    → Implementaciones concretas: Dapper, autenticación, cifrado.
GGH.API              → Controllers, middleware de manejo de errores, configuración.
```

## Cómo ejecutar el proyecto

### Con Docker

```bash
cp .env.example .env
# Edita .env con tus valores

docker compose up --build
```
API disponible en `http://localhost:7010`, Swagger en `http://localhost:7010/swagger`.

### En local, sin Docker

**Requisitos:** .NET 10 SDK, PostgreSQL 16+.

1. Crea una base de datos en PostgreSQL
2. Ejecuta los scripts de `Database/Scripts/` **en orden** (00 al 08)
3. Configura `GGH.API/appsettings.Development.json` con tu connection string, llave JWT y llave de cifrado
4. Desde `GGH.API/`:
   ```bash
   dotnet run
   ```

## Estructura del repositorio

```
.
├── docker-compose.yml
├── .env.example
├── GGH.API/                  # Punto de entrada, controllers, middleware
├── GGH.Application/          # Casos de uso (CQRS), interfaces, DTOs
├── GGH.Infrastructure/       # Dapper, JWT, cifrado, repositorios
├── GGH.Domain/               # Entidades, excepciones, value objects
├── GGH.*.Tests/               # Proyectos de pruebas unitarias
└── Database/Scripts/          # Scripts SQL (tablas, funciones, datos semilla)
```

## Documentación de la API

Con el proyecto corriendo, Swagger está disponible en `/swagger`.

## Licencia

Proyecto de portafolio personal, de uso libre con fines educativos.
