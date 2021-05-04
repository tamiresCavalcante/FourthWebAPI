using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FourthWebAPI.Models
{
    public class HttpClientWrapper : IHttpClient
    {
        private HttpClient _client = new HttpClient();
        public string Get(string url)
        {
            return GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            return PostAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }
    }
}
