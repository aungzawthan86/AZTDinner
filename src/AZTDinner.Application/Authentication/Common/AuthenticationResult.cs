using AZTDinner.Domain.Entities;

namespace AZTDinner.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);