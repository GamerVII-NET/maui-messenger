using Messenger.Server.Services;

namespace Messenger.Server.Helpers
{
    public class ServicesManager
    {

        internal static void InitServices(WebApplicationBuilder builder, WebApplication app)
        {
            app.MapPost("/api/v1/auth", AuthService.GetAccessToken(builder));

            app.MapGet("/api/v1/users", UserService.GetUsersList());
            app.MapGet("/api/v1/users/{guid}", UserService.GetUserByGuid());
            app.MapPost("/api/v1/users/", UserService.CreateUser());
            app.MapPut("/api/v1/users/", UserService.UpdateUser(builder));
        }
    }



}
