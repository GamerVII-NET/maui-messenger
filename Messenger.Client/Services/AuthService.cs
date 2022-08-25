using Messenger.Domains.Models;
using System.Net.Http.Json;

namespace Messenger.Client.Services
{
    public class AuthService
    {
        internal static async Task<HttpResponseMessage> AuthAsync(string login, string password)
        {

            User user = new User
            {
                UserName = login,
                Password = password
            };

            JsonContent content = JsonContent.Create(user);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("http://localhost:5064/api/v1/auth", content);

                var data = await response.Content.ReadAsStringAsync();

                return response;
            }

        }
    }
}
