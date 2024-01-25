using AZTDinner.Application.Authentication.Common;
using AZTDinner.Application.Common.Interfaces.Authentication;
using AZTDinner.Application.Common.Interfaces.Persistance;
using AZTDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AZTDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }
  public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
    if (_userRepository.GetUserByEmail(command.Email) is not null)
    {
      // throw new Exception("User with given email already exists");
      //  throw new DuplicateEmailExcetion();
      // return new DuplicateEmailError();
      // return Result.Fail<AuthenticationResult>(new[]{new DuplicateEmailError()});
      return Errors.User.DuplicateEmail;
    }
    var user = new User
    {
      FirstName = command.FirstName,
      LastName = command.LastName,
      Email = command.Email,
      Password = command.Password
    };
    _userRepository.Add(user);
    var token = _jwtTokenGenerator.GenerateToken(user);
    return new AuthenticationResult(user,
                                    token);
  }
}