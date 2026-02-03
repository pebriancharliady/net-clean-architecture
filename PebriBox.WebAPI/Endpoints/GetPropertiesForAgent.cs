using MediatR;
using PebriBox.Application.Features.Properties.Queries;
using PebriBox.Application.Wrappers;

namespace PebriBox.WebAPI.Endpoints;

public static class GetPropertiesForAgentEndpoint
{
    public static RouteHandlerBuilder MapGetPropertiesForAgentEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/agent/{id:int}", async (int id, ISender sender) =>
        {
            var command = new GetPropertiesByAgentQuery { AgentId = id };
            var result = await sender.Send(command);
            return Results.Ok(result);
        }).Produces<ResponseWrapper<int>>(StatusCodes.Status200OK).Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
    }
}
