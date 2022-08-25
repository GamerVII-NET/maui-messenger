using Messenger.Domains.Dtos.User;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Messenger.Client.Services
{
    public static class UserService
    {
        public static async Task<UserReadDto> GetUserInfo(string token, string guid)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"http://localhost:5064/api/v1/users/{guid}");

                var data = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<UserReadDto>(data);
            }
        }

        public static async Task<UserReadDto> UpdateUserInfoAsync(string token, UserUpdateDto user)
        {
            JsonContent content = JsonContent.Create(user);

            using (var client = new HttpClient())
            {
                var response = await client.PutAsync("http://localhost:5064/api/v1/users", content);

                var data = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<UserReadDto>(data);
            }
        }
    }
}
