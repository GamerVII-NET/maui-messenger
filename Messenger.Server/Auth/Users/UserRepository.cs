using Messenger.Domains.Models;

public class UserRepository : IUserRepository
{

    private readonly List<UserDto> _users = new List<UserDto>
    {
        new UserDto("test", "test")
    };

    public UserRepository()
    {

    }

    public UserDto GetUser(UserModel userModel) =>
        _users.FirstOrDefault(c =>
        string.Equals(c.UserName, userModel.UserName) &&
        string.Equals(c.Password, userModel.Password)) ??
        throw new Exception("User not found");


}