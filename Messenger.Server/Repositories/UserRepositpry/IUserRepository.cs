using Messenger.Domains.Dtos;

namespace Messenger.Server.Repositories.UserRepository
{
    public interface IUserRepository
    {

        Task SaveChangesAsync();

        Task<User?> GetUserByGuidAsync(Guid guid);

        Task<User?> GetUserByUserNameAsync(string userName);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> CreateUserAsync(User user);

        Task<User?> UpdateUserAsync(User user);

        Task<User?> AuthUserAsync(UserAuthDto user);

        void DeleteUser(User user);
    }
}
