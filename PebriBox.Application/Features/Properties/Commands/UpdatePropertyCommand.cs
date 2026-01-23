using Mapster;
using MediatR;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Models.Responses;
using PebriBox.Application.Wrappers;
using PebriBox.Domain.Entities;

namespace PebriBox.Application.Features.Properties.Commands;

public class UpdatePropertyCommand : IRequest<IResponseWrapper>
{
    public UpdatePropertyRequest UpdateProperty { get; set; }
}

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, IResponseWrapper>
{
    private readonly IPropertyService _propertyService;

    public UpdatePropertyCommandHandler(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IResponseWrapper> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _propertyService.UpdateAsync(request.UpdateProperty.Adapt<Property>());
        return ResponseWrapper<PropertyResponse>.Success(data: property.Adapt<PropertyResponse>(), message: "Property Updated");
    }
}
