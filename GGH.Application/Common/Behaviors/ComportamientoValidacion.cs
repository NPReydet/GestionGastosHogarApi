using FluentValidation;
using MediatR;

namespace GGH.Application.Common.Behaviors
{
    public class ComportamientoValidacion<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validadores;

        public ComportamientoValidacion(IEnumerable<IValidator<TRequest>> validadores)
        {
            _validadores = validadores;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> siguiente,
            CancellationToken cancellationToken)
        {
            if (!_validadores.Any())
            {
                return await siguiente();
            }

            var contexto = new ValidationContext<TRequest>(request);

            var resultados = await Task.WhenAll(
                _validadores.Select(v => v.ValidateAsync(contexto, cancellationToken)));

            var errores = resultados
                .SelectMany(r => r.Errors)
                .Where(e => e != null)
                .ToList();

            if (errores.Count != 0)
            {
                throw new ValidationException(errores);
            }

            return await siguiente();
        }
    }
}
