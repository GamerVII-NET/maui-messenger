public interface ITokenService
{
    string BuildToken(string key, string issuer, User user);

    bool VerifyToken(WebApplicationBuilder builder, HttpContext context, User user);
}