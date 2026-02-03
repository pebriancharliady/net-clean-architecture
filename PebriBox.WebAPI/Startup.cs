using PebriBox.WebAPI.Endpoints;
namespace PebriBox.WebAPI;

public static class Startup
{
    public static void MapPropertyEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var propertyGroup = endpoint.MapGroup("properties").WithTags("Properties");
        propertyGroup.MapCreatePropertyEndoint();
        propertyGroup.MapUpdatePropertyEndoint();
        propertyGroup.MapDeletePropertyEndpoint();
        propertyGroup.MapPropertyGetByIdEndpoint();
        propertyGroup.MapGetPropertiesEndpoint();
        propertyGroup.MapGetPropertiesForAgentEndpoint();
    }
}
