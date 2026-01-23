using Mapster;
using MediatR;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Properties.Queries;

public class GetPropertiesByAgentQuery : IRequest<IResponseWrapper<List<PropertyResponse>>>
{
    public int AgentId { get; set; }
}

public class GetPropertiesByAgentQueryHandler : IRequestHandler<GetPropertiesByAgentQuery, IResponseWrapper<List<PropertyResponse>>>
{
    private readonly IPropertyService _propertyService;

    public GetPropertiesByAgentQueryHandler(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IResponseWrapper<List<PropertyResponse>>> Handle(GetPropertiesByAgentQuery request, CancellationToken cancellationToken)
    {
        var properties = await _propertyService.GetByAgentIdAsync(request.AgentId);
        return ResponseWrapper<List<PropertyResponse>>.Success(data: properties.Adapt<List<PropertyResponse>>());
    }
}
