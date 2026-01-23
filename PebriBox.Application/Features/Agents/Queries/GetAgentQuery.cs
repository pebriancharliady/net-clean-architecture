using Mapster;
using MediatR;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Agents.Queries;

public class GetAgentQuery : IRequest<IResponseWrapper>
{
    public int AgentId { get; set; }
}

public class GetAgentQueryHandler : IRequestHandler<GetAgentQuery, IResponseWrapper>
{
    private readonly IAgentService _agentService;

    public GetAgentQueryHandler(IAgentService agentService)
    {
        _agentService = agentService;
    }

    public async Task<IResponseWrapper> Handle(GetAgentQuery request, CancellationToken cancellationToken)
    {
        var agent = await _agentService.GetByIdAsync(request.AgentId);
        if (agent == null)
        {
            return ResponseWrapper<AgentResponse>.Fail("Agent Not Found");
        }
        return ResponseWrapper<AgentResponse>.Success(data: agent.Adapt<AgentResponse>());
    }
}
