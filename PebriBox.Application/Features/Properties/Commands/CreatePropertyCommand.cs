using System;
using Mapster;
using MediatR;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Wrappers;
using PebriBox.Domain.Entities;

namespace PebriBox.Application.Features.Properties.Commands;

public class CreatePropertyCommand : IRequest<IResponseWrapper>
{
    public CreatePropertyRequest CreateProperty { get; set; }
}

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, IResponseWrapper>
{
    private readonly IPropertyService _propertyService;

    public CreatePropertyCommandHandler(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IResponseWrapper> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var propertyId = await _propertyService.CreateAsync(request.CreateProperty.Adapt<Property>());
        return ResponseWrapper<int>.Success(data: propertyId, message: "Property Created");
    }
}
