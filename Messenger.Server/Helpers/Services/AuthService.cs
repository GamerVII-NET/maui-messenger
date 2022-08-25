using Messenger.Domains.Dtos.User;
using Messenger.Server.Repositories.UserRepository;

namespace Messenger.Server.Helpers.Services
{
    public class AuthService
    {

        internal static Func<IUserRepository, ITokenService, UserAuthDto, Task<IResult>> GetAccesstoken(WebApplicationBuilder builder)
        {
            return async (IUserRepository repository, ITokenService tokenService, UserAuthDto user) =>
            {
                var userDto = await repository.AuthUserAsync(user);

                if (userDto == null) return Results.Unauthorized();

                var token = tokenService.BuildToken(
                    builder.Configuration["Jwt:Key"],
                    builder.Configuration["Jwt:Issuer"],
                    userDto
                    );

                //return Task.FromResult(Results.Ok(userDto));
                return Results.Ok(token);
            };
        }
    }
}
