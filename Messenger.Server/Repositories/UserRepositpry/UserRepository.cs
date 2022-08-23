using Messenger.Domains.Dtos;
using Messenger.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Server.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthUserAsync(UserAuthDto user)
        {
            var authUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (authUser == null)
            {
                return null;
            }

            return authUser;

        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.AddAsync(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users!.ToListAsync();
        }

        public async Task<User?> GetUserByGuidAsync(Guid guid)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.GlobalGuid == guid);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        //public UserDto GetUser(User userModel) =>
        //_users.FirstOrDefault(c =>
        //string.Equals(c.UserName, userModel.UserName) &&
        //string.Equals(c.Password, userModel.Password)) ??
        //throw new Exception("User not found");
    }
}
