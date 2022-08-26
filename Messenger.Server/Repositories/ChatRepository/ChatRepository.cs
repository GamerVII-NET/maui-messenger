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

        public Task<Chat> CreateChat(User user, Chat chat)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (chat == null)
            {
                throw new ArgumentNullException(nameof(chat));
            }


            throw new NotImplementedException();


        }

        public void DeleteChat(Chat chat)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chat>> GetAllChats()
        {
            throw new NotImplementedException();
        }

        public Task<Chat?> GetChatByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
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
