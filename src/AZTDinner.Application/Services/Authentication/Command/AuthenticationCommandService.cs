using AZTDinner.Application.Common.Errors;
using AZTDinner.Application.Common.Interfaces.Authentication;
using AZTDinner.Application.Common.Interfaces.Persistance;
using AZTDinner.Application.Serviecs.Authentication.Common;
using AZTDinner.Domain.Entities;
using ErrorOr;
using FluentResults;
//using OneOf;

namespace AZTDinner.Application.Serviecs.Authentication.Command;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null)
        {
           // throw new Exception("User with given email already exists");
         //  throw new DuplicateEmailExcetion();
        // return new DuplicateEmailError();
          // return Result.Fail<AuthenticationResult>(new[]{new DuplicateEmailError()});
          return Errors.User.DuplicateEmail;
        }
        var user = new User{
            FirstName=firstName,
            LastName=lastName,
            Email=email,
            Password=password
        };
       _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user);
       return new AuthenticationResult(user,
                                       token);
    }

}