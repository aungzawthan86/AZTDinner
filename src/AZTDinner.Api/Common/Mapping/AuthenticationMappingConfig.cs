using AZTDinner.Application.Authentication.Commands.Register;
using AZTDinner.Application.Authentication.Common;
using AZTDinner.Application.Authentication.Queries.Login;
using AZTDinner.Contracts.Authentication;
using Mapster;

namespace AZTDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest, source => source.User);
    }
}