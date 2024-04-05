using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyWeb.Models.ViewModel;

namespace SpotifyWeb.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginVM> PostLoginAsync(string url, LoginVM loginVM)
        {
            var loginJson = JsonConvert.SerializeObject(loginVM);
            var content = new StringContent(loginJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginVM>(jsonString);
        }
    }
}
