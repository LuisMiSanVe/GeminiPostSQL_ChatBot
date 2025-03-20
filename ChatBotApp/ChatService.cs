namespace ChatBotApp
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ChatService
    {
        // Connection parameters
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "";  // Replace with your API key
        // ChatBot parameters
        private readonly List<string> _conversationHistory = new();
        private const int MaxHistory = 5; // Keep only the last X messages

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseAsync(string userInput)
        {
            // Load the History
            string message = "This are the latest messages you sent me: (" + string.Join(", ",_conversationHistory) + ") use the previous messages to get context and answer this new request, dont talk about the past messages just use them for contextualizing: ";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = message + userInput } } }
                }
            };
            // Send the Request to Gemini
            var response = await _httpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}",
            requestBody
        );
            // Extract the response
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                using JsonDocument resp = JsonDocument.Parse(jsonResponse);
                string generatedSql = resp.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString().Replace("```sql", "").Replace("```", "").Replace('\n', ' ').Trim();

                // Save the last 5 messages in the history
                _conversationHistory.Insert(0, generatedSql);
                if (_conversationHistory.Count > MaxHistory)
                {
                    _conversationHistory.RemoveAt(_conversationHistory.Count - 1);
                }

                return generatedSql;
            }

            return "Error connecting to Gemini AI.";
        }
    }

    public class ChatResponse
    {
        public string Reply { get; set; }
    }

}
