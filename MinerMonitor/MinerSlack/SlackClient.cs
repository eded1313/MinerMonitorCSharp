using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MinerSlack
{
    public class SlackMessageOptionModel
    {
        public string color { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    }
    public abstract class SlackClient
    {
        private readonly string _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        public SlackClient(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public abstract Task<bool> ExecuteAsync(SlackMessageOptionModel message);

        protected async Task<HttpResponseMessage> SendMessageAsync(string message, string channel = null, string username = null)
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

        protected async Task<bool> SendMessageAsync(SlackMessageOptionModel option)
        {
            try
            {
                //string paramJson = System.Text.Json.JsonSerializer.Serialize(new { text = msg });
                List<SlackMessageOptionModel> options = new List<SlackMessageOptionModel>();
                options.Add(option);
                string paramJson = System.Text.Json.JsonSerializer.Serialize(new { attachments = options.ToArray() });

                // Payload
                var content = new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "payload", paramJson }
                });

                // POST!!
                HttpClient _httpClient = new System.Net.Http.HttpClient();

                System.Net.Http.HttpResponseMessage res = await _httpClient.PostAsync(_webhookUrl, content);

                return (res.StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"slack send massage error: {ex.Message}");
                return false;
            }
        }
    }
}
