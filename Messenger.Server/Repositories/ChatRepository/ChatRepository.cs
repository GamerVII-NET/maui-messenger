using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Server.Repositories.ChatRepository
{
    public class ChatRepository : IChatRepository
    {

        private readonly DataBaseContext _context;

        public ChatRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            return await _context.Chats!.ToListAsync();
        }

        public async Task<Chat?> AddUserToChat(User user, Chat chat)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (chat == null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            var checkChat = await _context.Chats
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.GlobalGuid == chat.GlobalGuid);
                
            if (checkChat == null) { return null; }

            var checkUserInChat = checkChat.Users.FirstOrDefault(c => c.User.GlobalGuid == user.GlobalGuid);

            if (checkUserInChat != null) { return checkChat; }

            user = await _context.Users.FirstOrDefaultAsync(c => c.GlobalGuid == user.GlobalGuid);

            var chatUser = new ChatUser
            {
                Chat = chat,
                UserRole = Domains.Enums.ChatRole.Default

            };

            var newUserChatModel = await _context.AddAsync(chatUser);

            newUserChatModel.Entity.User = user;

            chat.Users.Add(newUserChatModel.Entity);

            var chatModel = _context.Update(checkChat);

            if (chatModel == null)
            {
                return null;
            }

            return chatModel.Entity;
        }

        public async Task<Chat?> CreateChat(User user, Chat chat)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (chat == null)
            {
                throw new ArgumentNullException(nameof(chat));
            }
            
            var chatModel = await _context.Chats.AddAsync(chat);

            await _context.SaveChangesAsync();

            return await AddUserToChat(user, chatModel.Entity);

        }

        public void DeleteChat(Chat chat)
        {
            throw new NotImplementedException();
        }

        public Task<Chat?> GetChatByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Task<Chat?> SearchChatByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> UpdateChat(Chat chat)
        {
            throw new NotImplementedException();
        }
    }
}
