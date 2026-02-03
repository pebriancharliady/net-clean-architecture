using MediatR;
using PebriBox.Application.Features.Properties.Commands;
using PebriBox.Application.Models.Requests;
using PebriBox.Application.Wrappers;

namespace PebriBox.WebAPI.Endpoints;

public static class DeletePropertyEndpoint
{
    public static RouteHandlerBuilder MapDeletePropertyEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("{id:int}", async (int id, ISender sender) =>
        {
            var command = new DeletePropertyCommand
            {
                PropertyId = id
            };
            var result = await sender.Send(command);
            return Results.Ok(result);
        }).Produces<ResponseWrapper<int>>(StatusCodes.Status200OK).Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
    }
}
