using AZTDinner.Domain.Entities;

namespace AZTDinner.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);

}