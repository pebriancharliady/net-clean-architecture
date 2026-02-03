using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PebriBox.Application.Features.Pipelines;

namespace PebriBox.Application;

public static class Startup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly))
        .AddValidatorsFromAssembly(assembly)
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineValidator<,>));
        return services;
    }
}
