using System;
using Mapster;
using MediatR;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Wrappers;
using PebriBox.Domain.Entities;

namespace PebriBox.Application.Features.Agents.Commands;

public class CreateAgentCommand : IRequest<IResponseWrapper>
{
    public CreateAgentRequest CreateAgent { get; set; }
}

public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, IResponseWrapper>
{
    private readonly IAgentService _agentService;

    public CreateAgentCommandHandler(IAgentService agentService)
    {
        _agentService = agentService;
    }

    public async Task<IResponseWrapper> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        var agentId = await _agentService.CreateAsync(request.CreateAgent.Adapt<Agent>());
        return ResponseWrapper<int>.Success(data: agentId, message: "Agent Created");
    }
}
