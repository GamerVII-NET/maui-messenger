using Messenger.Domains.Dtos;
using Messenger.Domains.Models;

public interface ITokenService
{
    string BuildToken(string key, string issuer, User user);
}