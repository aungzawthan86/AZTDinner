using AZTDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace AZTDinner.Application.Authentication.Queries.Login;

public record LoginQuery(
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>;