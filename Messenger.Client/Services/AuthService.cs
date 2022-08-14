using Messenger.Domains.Models;
using System.Net.Http.Json;

namespace Messenger.Client.Services
{
    public class AuthService
    {
        internal static async Task<HttpResponseMessage> AuthAsync(string login, string password)
        {

            UserModel user = new UserModel
            {
                UserName = login,
                Password = password
            };

            JsonContent content = JsonContent.Create(user);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("http://localhost:5064/login", content);

                var data = await response.Content.ReadAsStringAsync();

                return response;
            }

        }
    }
}
