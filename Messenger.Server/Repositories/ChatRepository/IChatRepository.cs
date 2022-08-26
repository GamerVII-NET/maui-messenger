﻿namespace Messenger.Server.Repositories.ChatRepository
{
    public interface IChatRepository
    {
        Task SaveChanges();

        Task<Chat?> GetChatByGuid(Guid guid);

        Task<Chat?> SearchChatByName(string name);

        Task<Chat?> CreateChat(User user, Chat chat);

        Task<Chat?> AddUserToChat(User user, Chat chat);

        Task<Chat> UpdateChat(Chat chat);

        Task<IEnumerable<Chat>> GetAllChatsAsync();

        void DeleteChat(Chat chat);

    }
}