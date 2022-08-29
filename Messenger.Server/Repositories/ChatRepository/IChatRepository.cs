using Messenger.Domains.Enums;

namespace Messenger.Server.Repositories.ChatRepository
{
    public interface IChatRepository
    {
        Task SaveChanges();

        Task<Chat?> GetChatByGuidAsync(Guid guid);

        Task<Chat?> SearchChatByName(string name);

        Task<Chat?> CreateChat(User unviter, Chat chat, IEnumerable<User> user);

        Task<Chat?> AddUserToChat(User user, Chat chat, User inviter = null, ChatRole chatRole = ChatRole.Default);

        Task<Chat> UpdateChat(Chat chat);

        Task<IEnumerable<Chat>> GetAllChatsAsync();

        void DeleteChat(Chat chat);

        Task<Chat?> ExitUserFromChat(User userModel, Chat chatModel);
    }
}
