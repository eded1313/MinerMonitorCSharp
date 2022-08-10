using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MinerMonitor.Utils
{
    public class SlackClient
    {
        private readonly string _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        public SlackClient(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public async Task<HttpResponseMessage> SendMessageAsync(string message, string channel = null, string username = null)
        {
            var payload = new
            {
                text = message,
                channel,
                username,
            };
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var response = await _httpClient.PostAsync(_webhookUrl, new StringContent(serializedPayload, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            string paramJson = System.Text.Json.JsonSerializer.Serialize(new { text = message });

            // Payload
            var content = new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "payload", paramJson }
            });

            // POST!!
            using System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();

            System.Net.Http.HttpResponseMessage res = await _httpClient.PostAsync(_webhookUrl, content);

            return (res.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
