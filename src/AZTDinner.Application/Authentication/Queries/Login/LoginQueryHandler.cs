using AZTDinner.Application.Authentication.Common;
using AZTDinner.Application.Common.Interfaces.Authentication;
using AZTDinner.Application.Common.Interfaces.Persistance;
using AZTDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AZTDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCreditials;
        }
        if (user.Password != query.Password)
        {
            return new[] { Errors.Authentication.InvalidCreditials };
        }
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user,
                                        token);
    }
}