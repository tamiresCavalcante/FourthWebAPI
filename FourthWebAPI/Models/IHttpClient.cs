using System.Net.Http;

namespace FourthWebAPI.Models
{
    public interface IHttpClient
    {
        string Get(string url);
        string Post(string url, HttpContent content);
    }
}
