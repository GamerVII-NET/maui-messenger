using Messenger.Domains.Dtos.User;
using Messenger.Server.Repositories.UserRepository;

namespace Messenger.Server.Services
{
    public class AuthService
    {

        internal static Func<IUserRepository, ITokenService, UserAuthDto, Task<IResult>> GetAccessToken(WebApplicationBuilder builder)
        {
            return async (IUserRepository repository, ITokenService tokenService, UserAuthDto authUser) =>
            {
                var user = await repository.AuthUserAsync(authUser);

                if (user == null) return Results.Unauthorized();

                var token = tokenService.BuildToken(
                    builder.Configuration["Jwt:Key"],
                    builder.Configuration["Jwt:Issuer"],
                    user
                    );

                return Results.Json(new
                {
                    UserGuid = user.GlobalGuid,
                    Token = token
                });
            };
        }
    }
}
