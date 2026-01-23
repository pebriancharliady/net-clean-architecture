using Mapster;
using MediatR;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Agents.Queries;

public class GetAgents : IRequest<IResponseWrapper<List<AgentResponse>>>
{
}

public class GetAgentsQueryHandler : IRequestHandler<GetAgents, IResponseWrapper<List<AgentResponse>>>
{
    private readonly IAgentService _agentService;

    public GetAgentsQueryHandler(IAgentService agentService)
    {
        _agentService = agentService;
    }

    public async Task<IResponseWrapper<List<AgentResponse>>> Handle(GetAgents request, CancellationToken cancellationToken)
    {
        var agent = await _agentService.GetAllAsync();
        return ResponseWrapper<List<AgentResponse>>.Success(data: agent.Adapt<List<AgentResponse>>());
    }
}
