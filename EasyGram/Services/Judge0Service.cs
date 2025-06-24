using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyGram.Services
{
    public class Judge0Service
    {
        private readonly HttpClient _client;
        private const string ApiKey = "08b3375892msh140a9ceac694ad4p193443jsncbf406ac546e";
        private const string ApiHost = "judge0-ce.p.rapidapi.com";
        private const string ApiUrl = "https://judge0-ce.p.rapidapi.com/submissions?base64_encoded=false&wait=true";

        public Judge0Service()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", ApiKey);
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", ApiHost);
        }

        public async Task<string> RunPythonAsync(string code, string input)
        {
            var json = $@"{{
                ""language_id"": 71,
                ""source_code"": ""{code.Replace("\"", "\\\"")}"",
                ""stdin"": ""{input.Replace("\"", "\\\"")}"",
                ""wait"": true
            }}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiUrl, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
