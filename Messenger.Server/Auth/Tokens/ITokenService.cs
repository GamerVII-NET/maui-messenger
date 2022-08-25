public interface ITokenService
{
    string BuildToken(string key, string issuer, User user);

    bool VerifyToken(string key, string issuer, string token, string userName);
}