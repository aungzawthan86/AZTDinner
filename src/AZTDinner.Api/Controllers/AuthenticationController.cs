using AZTDinner.Application.Authentication.Commands.Register;
using AZTDinner.Application.Authentication.Common;
using AZTDinner.Application.Authentication.Queries.Login;
using AZTDinner.Application.Common.Errors;
using AZTDinner.Contracts.Authentication;
using ErrorOr;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace AZTDinner.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
               authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
               error => Problem(error));
        // return authResult.Match(
        //     authResult=>Ok(MapAuthResult(authResult)),
        //     _=>Problem(statusCode: StatusCodes.Status409Conflict, title:"Email already existed!")
        // );
        // Result<AuthenticationResult> regiserResult = _authenticationService.Register(
        //     request.FirstName,
        //     request.LastName,
        //     request.Email,
        //     request.Password
        // );
        // if(regiserResult.IsSuccess)
        // {
        //     return Ok(MapAuthResult(regiserResult.Value));
        // }
        // var firstError = regiserResult.Errors[0];
        // if(firstError is DuplicateEmailError)
        // {
        //     return Problem(statusCode:StatusCodes.Status409Conflict, detail:"Email already exists.");
        // }
        // return Problem();
        // return regiserResult.Match(
        //     authResult=>Ok(MapAuthResult(authResult)),
        //     error  =>Problem(statusCode:(int)error.StatusCode, title:error.ErrorMessage)
        // );

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCreditials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                        title: authResult.FirstError.Description);
        }
        return authResult.Match(
               authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
               error => Problem(error));
        // var response = new AuthenticationResponse(
        //     authResult.User.Id,
        //     authResult.User.FirstName,
        //     authResult.User.LastName,
        //     authResult.User.Email,
        //     authResult.Token
        // );
        // return Ok(response);
    }

}