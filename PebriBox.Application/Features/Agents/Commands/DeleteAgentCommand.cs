using MediatR;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Agents.Commands;

public class DeleteAgentCommand : IRequest<IResponseWrapper>
{
    public int AgentId { get; set; }
}

public class DeleteAgentCommandHandler : IRequestHandler<DeleteAgentCommand, IResponseWrapper>
{
    private readonly IAgentService _agentService;

    public DeleteAgentCommandHandler(IAgentService agentService)
    {
        _agentService = agentService;
    }

    public async Task<IResponseWrapper> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
    {
        var agentId = await _agentService.DeleteAsync(request.AgentId);
        return ResponseWrapper<int>.Success(data: agentId, message: "Agent Deleted");
    }
}
