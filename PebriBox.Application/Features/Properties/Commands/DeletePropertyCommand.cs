using MediatR;
using PebriBox.Application.Wrappers;

namespace PebriBox.Application.Features.Properties.Commands;

public class DeletePropertyCommand : IRequest<IResponseWrapper>
{
    public int PropertyId { get; set; }
}

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, IResponseWrapper>
{
    private readonly IPropertyService _propertyService;

    public DeletePropertyCommandHandler(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IResponseWrapper> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var propertyId = await _propertyService.DeleteAsync(request.PropertyId);
        return ResponseWrapper<int>.Success(data: propertyId, message: "Property Deleted");
    }
}
