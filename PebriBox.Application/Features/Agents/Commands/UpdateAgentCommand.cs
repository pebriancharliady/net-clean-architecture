using Mapster;
using MediatR;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;
using PebriBox.Domain.Entities;

namespace PebriBox.Application.Features.Agents.Commands;

public class UpdateAgentCommand : IRequest<IResponseWrapper>
{
    public UpdateAgentRequest UpdateAgent { get; set; }
}

public class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, IResponseWrapper>
{
    private readonly IAgentService _agentService;

    public UpdateAgentCommandHandler(IAgentService agentService)
    {
        _agentService = agentService;
    }

    public async Task<IResponseWrapper> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
    {
        var agent = await _agentService.UpdateAsync(request.UpdateAgent.Adapt<Agent>());
        return ResponseWrapper<AgentResponse>.Success(data: agent.Adapt<AgentResponse>(), message: "Agent Updated");
    }
}
