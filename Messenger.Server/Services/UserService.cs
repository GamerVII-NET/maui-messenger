using AutoMapper;
using Messenger.Domains.Dtos.User;
using Messenger.Server.Repositories.UserRepository;
using Microsoft.Net.Http.Headers;

namespace Messenger.Server.Services
{
    public class UserService
    {
        internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsersList()
        {
            return [Authorize] async (IUserRepository repository, IMapper mapper) =>
            {
                var users = await repository.GetAllUsersAsync();

                return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(users));
            };
        }

        internal static Func<IUserRepository, IMapper, Guid, Task<IResult>> GetUserByGuid()
        {
            return [Authorize] async (IUserRepository repository, IMapper mapper, Guid guid) =>
            await repository.GetUserByGuidAsync(guid) is User userModel
                ? Results.Ok(mapper.Map<UserReadDto>(userModel))
            : Results.NotFound();
        }

        internal static Func<IUserRepository, IMapper, UserCreateDto, Task<IResult>> CreateUser()
        {
            return [Authorize] async (IUserRepository repository, IMapper mapper, UserCreateDto user) =>
            {
                User userModel = mapper.Map<User>(user);

                var checkUser = await repository.GetUserByUserNameAsync(user.UserName);

                if (checkUser != null)
                {
                    return Results.Conflict();
                }

                userModel = await repository.CreateUserAsync(userModel);

                await repository.SaveChangesAsync();

                return Results.Created($"/api/v1/users/{userModel.GlobalGuid}", userModel);
            };
        }

        internal static Func<HttpContext, IUserRepository, ITokenService, IMapper, UserUpdateDto, Task<IResult>> UpdateUser(WebApplicationBuilder builder)
        {
            return [Authorize] async (HttpContext context, IUserRepository repository, ITokenService tokenService, IMapper mapper, UserUpdateDto user) =>
            {
                var userModel = mapper.Map<User>(user);

                var isCurrentUser = tokenService.VerifyToken(
                    builder.Configuration["Jwt:Key"],
                    builder.Configuration["Jwt:Issuer"],
                    context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""),
                    userModel.UserName
                    );

                if (isCurrentUser == false) { return Results.Unauthorized(); }

                var checkUser = await repository.GetUserByGuidAsync(userModel.GlobalGuid);

                if (checkUser == null)
                {
                    return Results.NotFound();
                }

                userModel = await repository.UpdateUserAsync(userModel);

                await repository.SaveChangesAsync();

                return Results.Ok(mapper.Map<UserReadDto>(userModel));

            };
        }
    }
}
