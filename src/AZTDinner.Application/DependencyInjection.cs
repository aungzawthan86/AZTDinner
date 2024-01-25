using AZTDinner.Application.Serviecs.Authentication;
using AZTDinner.Application.Serviecs.Authentication.Command;
using AZTDinner.Application.Serviecs.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using AZTDinner.Application.Authentication.Commands.Register;
using AZTDinner.Application.Authentication.Common;
using System.ComponentModel.DataAnnotations;
using AZTDinner.Application.Common.Behaviors;
using ErrorOr;
using FluentValidation;
namespace AZTDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AppApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
        // services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>,
        // ValidateRegisterCommandBehavior>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}