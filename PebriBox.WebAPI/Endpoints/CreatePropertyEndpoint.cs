using MediatR;
using PebriBox.Application.Features.Properties.Commands;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Wrappers;

namespace PebriBox.WebAPI.Endpoints;

public static class CreatePropertyEndpoint
{
    public static RouteHandlerBuilder MapCreatePropertyEndoint(this IEndpointRouteBuilder app)
    {
        return app.MapPost("add", async (CreatePropertyRequest createPropertyRequest, ISender sender) =>
        {
            var command = new CreatePropertyCommand
            {
                CreateProperty = createPropertyRequest
            };
            var result = await sender.Send(command);
            return Results.Ok(result);
        }).Produces<ResponseWrapper<int>>(StatusCodes.Status200OK);
    }
}
