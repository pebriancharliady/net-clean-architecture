using MediatR;
using PebriBox.Application.Features.Properties.Queries;
using PebriBox.Application.Wrappers;

namespace PebriBox.WebAPI.Endpoints;

public static class GetPropertiesEndpoint
{
    public static RouteHandlerBuilder MapGetPropertiesEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/", async (ISender sender) =>
        {
            var command = new GetPropertiesQuery { };
            var result = await sender.Send(command);
            return Results.Ok(result);
        }).Produces<ResponseWrapper<int>>(StatusCodes.Status200OK).Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
    }
}
