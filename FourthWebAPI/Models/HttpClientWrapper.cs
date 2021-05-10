using System.Net.Http;

namespace FourthWebAPI.Models
{
    public class HttpClientWrapper : IHttpClient
    {
        private HttpClient _client = new HttpClient();

        public string Get(string url)
        {
            return _client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public string Post(string url, HttpContent content)
        {
            return _client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
        }
    }
}
