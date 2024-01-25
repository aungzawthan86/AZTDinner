using AZTDinner.Application.Serviecs.Authentication;
using AZTDinner.Application.Serviecs.Authentication.Command;
using AZTDinner.Application.Serviecs.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using AZTDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AZTDinner.Api.Common.Errors;
namespace AZTDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AppPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, AZTDinnerProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }

}