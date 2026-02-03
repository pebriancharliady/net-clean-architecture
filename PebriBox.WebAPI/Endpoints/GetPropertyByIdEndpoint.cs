using MediatR;
using PebriBox.Application.Features.Properties.Queries;
using PebriBox.Application.Wrappers;

namespace PebriBox.WebAPI.Endpoints;

public static class GetPropertyByIdEndoint
{
    public static RouteHandlerBuilder MapPropertyGetByIdEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            var command = new GetPropertyQuery
            {
                PropertyId = id
            };
            var result = await sender.Send(command);
            return Results.Ok(result);
        }).Produces<ResponseWrapper<int>>(StatusCodes.Status200OK).Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
    }
}
