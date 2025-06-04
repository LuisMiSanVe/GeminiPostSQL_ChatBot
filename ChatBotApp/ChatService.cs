namespace ChatBotApp
{
    using Npgsql;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ChatService
    {
        // Connection parameters
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "";  // Replace with your API key
        // Database Data
        public static string database = "Host=localhost;Username=username;Password=password;Database=database";
        // ChatBot parameters
        private readonly List<string> _conversationHistory = new();
        private const int MaxHistory = 5; // Keep only the last X messages

        private string json = "";

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseAsync(string userInput)
        {
            // Connects to the database
            var connection = new NpgsqlConnection(database);

            string result = "";

            if (connection != null)
            {
                connection.Open();

                // // If the database is already mapped, it skips the process
                if (json == "")
                {
                    // OBTAIN DB
                    // Tables
                    var tablesDB = new NpgsqlCommand("SELECT CONCAT(table_schema, '.', table_name) AS full_table_name " +
                                                    "FROM information_schema.tables WHERE table_type = 'BASE TABLE' AND table_name NOT LIKE 'pg_%' AND table_name NOT LIKE 'sql_%' " +
                                                    "ORDER BY full_table_name;", connection).ExecuteReader();
                    // Table           Column(Type)
                    Dictionary<string, List<string>> tables = new Dictionary<string, List<string>>();

                    while (tablesDB.Read())
                    {
                        if (!tables.ContainsKey(tablesDB.GetString(0)))
                            //         Name                   Columns
                            tables.Add(tablesDB.GetString(0), null);
                    }
                    tablesDB.Close();
                    // Columns
                    foreach (string tableName in tables.Keys)
                    {
                        var columnsDB = new NpgsqlCommand("SELECT c.column_name, c.data_type, CASE WHEN tc.constraint_type = 'PRIMARY KEY' THEN 'PK' WHEN tc.constraint_type = 'FOREIGN KEY' THEN 'FK' ELSE '' END AS key_type " +
                                                        "FROM information_schema.columns c " +
                                                        "LEFT JOIN information_schema.key_column_usage kcu ON c.table_schema = kcu.table_schema AND c.table_name = kcu.table_name AND c.column_name = kcu.column_name " +
                                                        "LEFT JOIN information_schema.table_constraints tc ON kcu.constraint_name = tc.constraint_name AND kcu.table_schema = tc.table_schema AND kcu.table_name = tc.table_name " +
                                                        "WHERE c.table_schema = '" + tableName.Substring(0, tableName.IndexOf('.')) + "' AND c.table_name = '" + tableName.Remove(0, tableName.IndexOf('.') + 1) + "'" +
                                                        "ORDER BY c.column_name;", connection).ExecuteReader();

                        List<string> columns = new List<string>();

                        while (columnsDB.Read())
                        {
                            string columnInfo = columnsDB.GetString(0) + "(" + columnsDB.GetString(1) + ")";
                            if (!columnsDB.GetString(2).Equals(""))
                                columnInfo = columnsDB.GetString(0) + "(" + columnsDB.GetString(1) + ") (" + columnsDB.GetString(2) + ")";

                            if (!columns.Contains(columnInfo))
                            {   //      Name(Type)(Key)
                                columns.Add(columnInfo);

                                tables[tableName] = columns;
                            }
                        }
                        columnsDB.Close();
                    }
                    var opcions = new JsonSerializerOptions
                    {
                        WriteIndented = true // JSON format
                    };

                    json = System.Text.Json.JsonSerializer.Serialize(tables, opcions);
                }
                string history = "For contextualizing, this are the latest messages exchanged: (" + string.Join(", ", _conversationHistory);

                // Creates context to modify AI's behavior
                string contextSQL = "You're a database assistant, I'll send you requests and you'll return a PostgeSQL query to do my request and if what I request can't be found on the database, tell me, but don't use more words. " +
                                     history +
                                     "This is the database: " + json +
                                     "\nAnd this is my request: " + userInput;

                // Send the SQL Generation query
                var requestBodySQL = new
                {
                    contents = new[]
                    {
                    new { parts = new[] { new { text = contextSQL } } }
                    }
                };
                var responseSQL = await _httpClient.PostAsJsonAsync(
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}",
                    requestBodySQL
                );

                // Extract the response
                string generatedSql = "";
                if (responseSQL.IsSuccessStatusCode)
                {
                    var jsonResponse = await responseSQL.Content.ReadAsStringAsync();

                    using JsonDocument resp = JsonDocument.Parse(jsonResponse);
                    generatedSql = resp.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString().Replace("```sql", "").Replace("```", "").Replace('\n', ' ').Trim();

                    // Save the last 5 exchanges in the history
                    _conversationHistory.Insert(0, "Request:" + userInput + ";Response:" + generatedSql);
                    if (_conversationHistory.Count > MaxHistory)
                    {
                        _conversationHistory.RemoveAt(_conversationHistory.Count - 1);
                    }
                }

                try
                {
                    var resultBBDD = new NpgsqlCommand(generatedSql, connection).ExecuteReader();
                    var resultList = new List<Dictionary<string, object>>();

                    while (resultBBDD.Read())
                    {
                        Dictionary<string, object> resultDic = new Dictionary<string, object>();

                        for (int i = 0; i < resultBBDD.FieldCount; i++)
                        {
                            resultDic[resultBBDD.GetName(i)] = resultBBDD.GetValue(i);
                        }

                        resultList.Add(resultDic);
                    }
                    resultBBDD.Close();

                    result = System.Text.Json.JsonSerializer.Serialize(resultList, opcions);
                }
                catch (Exception e)
                {
                    result = "An error was thrown while running the generated query (" + generatedSql + ")\n" + e.StackTrace;
                }

                // reates context to modify AI's behavior
                string contextText = "You're an SQL Table interpreter, there is this SQL query that fulfills this request sent:" + userInput + ", and this is the result in JSON format: " + result +
                                     "With that data, describe the result in natural language and in the same language as the request, be concise.";

                // Send the results to Gemini to process it to text
                var requestBody = new
                {
                    contents = new[]
                    {
                    new { parts = new[] { new { text = contextText } } }
                    }
                };
                var response = await _httpClient.PostAsJsonAsync(
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}",
                    requestBody
                );
                // Extract the response
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    using JsonDocument resp = JsonDocument.Parse(jsonResponse);
                    string generatedText = resp.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();

                    return generatedText;
                }
            }
            
            return "Error connecting to Gemini AI.";
        }
    }

    public class ChatResponse
    {
        public string Reply { get; set; }
    }

}
