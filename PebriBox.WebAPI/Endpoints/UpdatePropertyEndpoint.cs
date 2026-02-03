using MediatR;
using PebriBox.Application.Features.Properties.Commands;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Wrappers;

namespace PebriBox.WebAPI.Endpoints;

public static class UpdatePropertyEndpoint
{
    public static RouteHandlerBuilder MapUpdatePropertyEndoint(this IEndpointRouteBuilder app)
    {
        return app.MapPut("/update", async (UpdatePropertyRequest updatePropertyRequest, ISender sender) =>
        {
            var command = new UpdatePropertyCommand
            {
                UpdateProperty = updatePropertyRequest
            };
            var result = await sender.Send(command);
            return Results.Ok(result);
        }).Produces<ResponseWrapper<int>>(StatusCodes.Status200OK).Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
    }
}
