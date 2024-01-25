using AZTDinner.Application.Common.Errors;
using AZTDinner.Application.Serviecs.Authentication.Common;
using ErrorOr;
using FluentResults;
//using OneOf;

namespace AZTDinner.Application.Serviecs.Authentication.Command;

public interface IAuthenticationCommandService
{    
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string Email, string Password);
}