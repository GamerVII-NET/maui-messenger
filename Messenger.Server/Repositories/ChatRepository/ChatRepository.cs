using Messenger.Domains.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Chat?> AddUserToChat(User user, Chat chat, User inviter = null, ChatRole chatRole = ChatRole.Default)
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
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(c => c.GlobalGuid == chat.GlobalGuid);

            if (checkChat == null) { return null; }

            var checkUserInChat = checkChat.Users.FirstOrDefault(c => c.User.GlobalGuid == user.GlobalGuid);

            if (checkUserInChat != null)
            {
                checkUserInChat.Deleted = false;
                return checkChat;
            }

            user = await _context.Users.FirstOrDefaultAsync(c => c.GlobalGuid == user.GlobalGuid);

            var chatUser = new ChatUser
            {
                Chat = chat,
                UserRole = Domains.Enums.ChatRole.Default
            };

            var newUserChatModel = await _context.AddAsync(chatUser);

            newUserChatModel.Entity.User = user;
            newUserChatModel.Entity.UserRole = chatRole;

            if (inviter != null)
            {
                var inviterModel = user = await _context.Users.FirstOrDefaultAsync(c => c.GlobalGuid == inviter.GlobalGuid);
                newUserChatModel.Entity.InviterUser = inviterModel;
            }

            var chatModel = _context.Update(checkChat);

            if (chatModel == null)
            {
                return null;
            }

            return chatModel.Entity;
        }

        public async Task<Chat?> CreateChat(User inviter, Chat chat, IEnumerable<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            if (chat == null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            var chatModel = await _context.Chats.AddAsync(chat);

            await _context.SaveChangesAsync();

            switch (chat.Type)
            {
                case Enums.ChatType.Conversation:
                    if (users.Count() < 1)
                    {
                        throw new Exception("No end users specified");
                    }

                    foreach (var user in users)
                    {
                        await AddUserToChat(user, chatModel.Entity, inviter);
                    }

                    chat = await AddUserToChat(inviter, chatModel.Entity, chatRole: ChatRole.Author);

                    break;

                case Enums.ChatType.Direct:

                    if (users.Count() != 1)
                    {
                        throw new Exception("No end user specified");
                    }

                    await AddUserToChat(users.FirstOrDefault(), chatModel.Entity, inviter);

                    chat = await AddUserToChat(inviter, chatModel.Entity, chatRole: ChatRole.Author);

                    break;

                case Enums.ChatType.Channel:
                    break;
            }

            return chat;

        }

        public void DeleteChat(Chat chat)
        {
            throw new NotImplementedException();
        }

        public async Task<Chat?> GetChatByGuidAsync(Guid guid)
        {
            return await _context.Chats.FirstOrDefaultAsync(c => c.GlobalGuid == guid);
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

        public async Task<Chat?> ExitUserFromChat(User user, Chat chat)
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
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(c => c.GlobalGuid == chat.GlobalGuid);

            if (checkChat == null) { return null; }

            var checkUserInChat = checkChat.Users.FirstOrDefault(c => c.User.GlobalGuid == user.GlobalGuid);

            if (checkUserInChat == null) { return checkChat; }

            checkUserInChat.Deleted = true;

            return checkChat;
        }
    }
}
