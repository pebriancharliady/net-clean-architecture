using System;
using FluentValidation;
using PebriBox.Application.Features.Agents;
using PebriBox.Application.Features.Properties.Commands;

namespace PebriBox.Application.Features.Properties.Validators;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator(IAgentService agentService)
    {
        RuleFor(command => command.CreateProperty)
            .SetValidator(new CreatePropertyRequestValidator(agentService));
    }
}
