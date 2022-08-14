using Messenger.Domains.Models;
public interface IUserRepository
{
    UserDto GetUser(UserModel userModel);
}