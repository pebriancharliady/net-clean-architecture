using FluentValidation;
using MediatR;
using PebriBox.Application.Features.Pipelines.Contracts;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Pipelines;

public class ValidatorPipelineValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IValidatable
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorPipelineValidator(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            if (!validationResults.Any(vr => vr.IsValid))
            {
                var messages = new List<string>();
                var failures = validationResults.SelectMany(vr => vr.Errors).Where(f => f != null).ToList();
                foreach (var failure in failures)
                {
                    messages.Add(failure.ErrorMessage);
                }
                IResponseWrapper response = ResponseWrapper.Fail(messages: messages);
                return (TResponse)response;
            }

        }
        return await next();
    }
}
