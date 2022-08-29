using Messenger.Domains.Dtos.User;
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
            // ToDo: make a normal check authorization
            var authUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (authUser == null)
            {
                return null;
            }

            return authUser;

        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userModel = await _context.AddAsync(user);

            return userModel.Entity;
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
            return await _context.Users
                .Include(c => c.UserChats)
                .ThenInclude(c => c.Chat)
                .FirstOrDefaultAsync(x => x.GlobalGuid == guid);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userModel = await _context.Users.FirstOrDefaultAsync(c => c.GlobalGuid == user.GlobalGuid);

            if (userModel == null) { return null; }

            userModel.UserName = user.UserName;
            userModel.FirstName = user.FirstName;
            userModel.LastName = user.LastName;
            userModel.Photo = user.Photo;
            userModel.Patronymic = user.Patronymic;
            userModel.Password = user.Password;
            userModel.LastOnline = DateTime.Now;

            return userModel;
        }
    }
}
