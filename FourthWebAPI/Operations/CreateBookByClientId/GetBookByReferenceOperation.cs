using FourthWebAPI.Models;
using FourthWebAPI.Models.Contracts;
using FourthWebAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Operations.CreateBookByClientId
{
    public class GetBookByReferenceOperation 
    {
        public GetBookByReferenceOperation(IHttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public IHttpClient HttpClient { get; set; }      

        public Book GetBookByReference(string reference, string url)
        {
            string book = this.HttpClient.Get($"{url}/{reference}");
            var bookResponse = JsonConvert.DeserializeObject<ClientContract>(book);
            return new Book
            {
                Author = bookResponse.Author
            };

        }
    }
}
