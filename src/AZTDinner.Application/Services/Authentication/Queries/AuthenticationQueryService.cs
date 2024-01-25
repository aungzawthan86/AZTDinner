using AZTDinner.Application.Common.Errors;
using AZTDinner.Application.Common.Interfaces.Authentication;
using AZTDinner.Application.Common.Interfaces.Persistance;
using AZTDinner.Application.Serviecs.Authentication.Common;
using AZTDinner.Domain.Entities;
using ErrorOr;
using FluentResults;
//using OneOf;

namespace AZTDinner.Application.Serviecs.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
   
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCreditials;
        }
        if(user.Password != password)
        {
           return new []{Errors.Authentication.InvalidCreditials};
        }
        var token = _jwtTokenGenerator.GenerateToken(user);
       return new AuthenticationResult(user,
                                       token);
    }
}