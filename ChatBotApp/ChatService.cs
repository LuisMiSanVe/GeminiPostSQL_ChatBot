namespace ChatBotApp
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class ChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseAsync(string userInput)
        {
            var response = await _httpClient.PostAsJsonAsync("http://192.168.1.4:5025/swagger/index.html", new { message = userInput });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ChatResponse>();
                return result?.Reply ?? "No response from AI.";
            }
            return "Error connecting to AI.";
        }
    }

    public class ChatResponse
    {
        public string Reply { get; set; }
    }

}
