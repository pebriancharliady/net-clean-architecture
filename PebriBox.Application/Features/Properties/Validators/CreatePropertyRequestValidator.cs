using FluentValidation;
using PebriBox.Application.Features.Agents;
using PebriBox.Application.Models.Requests;

namespace PebriBox.Application.Features.Properties.Validators;

public class CreatePropertyRequestValidator : AbstractValidator<CreatePropertyRequest>
{
    public CreatePropertyRequestValidator(IAgentService agentService)
    {
        RuleFor(req => req.ShortDescription)
            .NotEmpty().WithMessage("Property name is required.")
            .MaximumLength(100).WithMessage("Property name must not exceed 100 characters.");

        RuleFor(req => req.Price)
            .GreaterThan(0).WithMessage("Property price must be greater than zero.");

        RuleFor(req => req.AgentId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Agent ID is required.")
            .MustAsync(async (agentId, cancellation) => await agentService.DoesExistAsync(agentId))
            .WithMessage("Agent with the specified ID does not exist.");
    }
}
