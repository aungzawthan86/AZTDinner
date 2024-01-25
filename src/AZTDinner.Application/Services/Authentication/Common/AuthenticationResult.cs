using AZTDinner.Domain.Entities;

namespace AZTDinner.Application.Serviecs.Authentication.Common;
public record AuthenticationResult(
    User User,
    string Token
);