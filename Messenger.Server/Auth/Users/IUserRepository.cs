using Messenger.Domains.Models;
public interface IUserRepository
{
    UserDto GetUser(User userModel);
}