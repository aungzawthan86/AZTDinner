using AZTDinner.Application.Common.Errors;
using AZTDinner.Application.Serviecs.Authentication.Common;
using ErrorOr;
using FluentResults;
//using OneOf;

namespace AZTDinner.Application.Serviecs.Authentication.Queries;

public interface IAuthenticationQueryService
{
    
    ErrorOr<AuthenticationResult> Login(string Email, string Password);
}