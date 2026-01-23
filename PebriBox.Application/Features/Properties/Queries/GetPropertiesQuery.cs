using Mapster;
using MediatR;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Properties.Queries;

public class GetPropertiesQuery : IRequest<IResponseWrapper<List<PropertyResponse>>>
{
}

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IResponseWrapper<List<PropertyResponse>>>
{
    private readonly IPropertyService _propertyService;

    public GetPropertiesQueryHandler(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IResponseWrapper<List<PropertyResponse>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = await _propertyService.GetAllAsync();
        return ResponseWrapper<List<PropertyResponse>>.Success(data: properties.Adapt<List<PropertyResponse>>());
    }
}
