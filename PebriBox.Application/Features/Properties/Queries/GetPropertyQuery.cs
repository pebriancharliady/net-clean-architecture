using Mapster;
using MediatR;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Properties.Queries;

public class GetPropertyQuery : IRequest<IResponseWrapper>
{
    public int PropertyId { get; set; }
}

public class GetPropertyQueryHandler : IRequestHandler<GetPropertyQuery, IResponseWrapper>
{
    private readonly IPropertyService _propertyService;

    public GetPropertyQueryHandler(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IResponseWrapper> Handle(GetPropertyQuery request, CancellationToken cancellationToken)
    {
        var property = await _propertyService.GetByIdAsync(request.PropertyId);
        if (property == null)
        {
            return ResponseWrapper<PropertyResponse>.Fail("Property Not Found");
        }
        return ResponseWrapper<PropertyResponse>.Success(data: property.Adapt<PropertyResponse>());
    }
}
