using Autofac.Extras.Moq;
using FourthWebAPI.Controllers;
using FourthWebAPI.Models;
using FourthWebAPI.Operations.CreateBook;
using FourthWebAPI.Operations.CreateBookByClientId;
using FourthWebAPI.Operations.GetBook;
using FourthWebAPI.Operations.GetBookId;
using FourthWebAPI.Operations.RemoveBook;
using FourthWebAPI.Operations.UpdateBook;
using FourthWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FourthWebApi.UnitTest
{
    public class BookServiceTest
    {
        private readonly Mock<IBookService> BookServiceMock = new Mock<IBookService>();
        private readonly Mock<IHttpClient> ClientMock = new Mock<IHttpClient>();
        //private static readonly HttpClient Client = new HttpClient();

        [Fact]
        public void Get_ReturnsTrue_IfTheCollectionIsNotEmpty()
        {
            //Arrange
            BookServiceMock.Setup(x => x.Get()).Returns(new List<Book> { 
            new Book { Author = It.IsAny<string>() } });
            var operation = new GetBookOperation(BookServiceMock.Object);

            //Act
            var result = operation.GetBooks();

            //Assert
            Assert.NotEmpty(result);
            BookServiceMock.Verify(x => x.Get(), Times.Once);
        }
        
        [Fact]
        public void GetId_ReturnsTrue_IfItIsNull()
        {
            //Arrange
            BookServiceMock.Setup(p => p.Get(It.IsAny<string>())).Returns(new Book { Price = It.IsAny<decimal>()});
            Book book = new Book();
            var operation = new GetBookIdOperation(BookServiceMock.Object);
            //Act            
            var result = operation.GetBookId(It.IsAny<string>());
        
            //Assert
            Assert.Null(result.Author);

        }

        [Fact]
        public void GetId_ReturnsTrue_IfItIsNotEmpty()
        {
            //Arrange
            BookServiceMock.Setup(p => p.Get(It.IsAny<string>())).Returns(new Book { Id = "3", Price = 5 , Author = "Robert" });
            Book book = new Book();
            var operation = new GetBookIdOperation(BookServiceMock.Object);
            //Act            
            var result = operation.GetBookId("3");

            //Assert
            Assert.NotEmpty(result.Author);

        }

        [Fact]
        public void GetId_ReturnsTrue_IfNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                BookServiceMock.DefaultValueProvider = null;
            });
            var operation = new GetBookIdOperation(BookServiceMock.Object);
        }

        [Fact]
        public void Get_ReturnsTrue_IfResultNotEmpty()
        {
            //Arrange   
            BookServiceMock.Setup(x => x.Get()).Returns(new List<Book> { new Book { Id = It.IsIn<string>() } });
            var operation = new GetBookOperation(BookServiceMock.Object);

            //Act
            var result = operation.GetBooks();

            //Assert
            Assert.NotEmpty(result); 
        }

        [Fact]
        public void CreateBook_ReturnTrue_IfNotNull()
        {
            //Arrange
            var book = new Book();
            BookServiceMock.Setup(x => x.Create(book)).Returns(new Book { Category = It.IsAny<string>() });
            var operation = new CreateBookOperation(BookServiceMock.Object);

            //Act
            var result = operation.CreateBooks(book);

            //Assert
            Assert.NotNull(result);
            BookServiceMock.Verify(x => x.Create(book), Times.Once);
        }

        [Fact]
        public void CreateBook_ReturnTrue_IfEqual()
        {
            //Arrange
            Book book = new Book { BookName = "C# for dummies", Price = 6, Author = "Tim", Category = "Education" };
            BookServiceMock.Setup(x => x.Create(book)).Returns(new Book { Author = "Tim" });
            var operation = new CreateBookOperation(BookServiceMock.Object);

            //Act
            var result = operation.CreateBooks(book);

            //Assert
            Assert.Equal("Tim", result.Author);
        }
        
        [Fact]
        public void UpdateBooks_ReturnTrue_IfTheMethodIsCalled()
        {
            
            BookServiceMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Book>()));
            var operation = new UpdateBookOperation(BookServiceMock.Object);
            operation.UpdateBooks(It.IsAny<string>(), It.IsAny<Book>());
            BookServiceMock.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Book>()), Times.Once);  
        }

        [Fact]
        public void RemoveBooks_ReturnTrue_IfTheMethodIsCalled()
        {
            BookServiceMock.Setup(x => x.Remove(It.IsAny<string>()));
            var operation = new RemoveBookOperation(BookServiceMock.Object);
            operation.RemoveBooks(It.IsAny<string>());
            BookServiceMock.Verify(X => X.Remove(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void EndPointGet_ReturnsTrue_IfEndPointWorks()
        {
            HttpClient httpClient = new HttpClient();
            string getUrl = "https://localhost:5001/api/books";
            var result = httpClient.GetAsync(getUrl);

            httpClient.Dispose();
            Assert.NotNull(result);
        }
        
        [Fact]
        public void HttpResponse_ReturnsTrue_IfNotNull()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            string getUrl = "https://localhost:5001/api/books";
            httpRequestMessage.RequestUri = new Uri(getUrl);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

            Assert.NotNull(httpResponse);
        }

        //[Fact]
        //public void Testing_httpclient4()
        //{
        //    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        //    handlerMock.Protected()
        //        .Setup<Task<HttpResponseMessage>>(
        //        "SendAsync",
        //        ItExpr.IsAny<HttpRequestMessage>(),
        //        ItExpr.IsAny<CancellationToken>()
        //        )
        //        .ReturnsAsync(new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent(""),
        //        }).Verifiable();

        //    var httpClient = new HttpClient(handlerMock.Object)
        //    {
        //        BaseAddress = new Uri("https://localhost:5001/api/books"),
        //    };

        //    var subjectUnderTest = new BookServiceTest(httpClient);
        //    var result = await subjectUnderTest.GetSomethingRemoteAsync()
        //}
        /*
        [Fact]
        public async Task HttpClient_CallSendAsyncAtLeastOnce()
        {
            var httpClient = new Mock<HttpClient>();
            httpClient.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), CancellationToken.None))
                .ReturnsAsync(
                new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{}")
                })
                .Verifiable();

            var classToTest = new BooksController(httpClient.Object);
            var response = classToTest.Get();
            httpClient.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), CancellationToken.None), Times.AtLeastOnce);
        }*/
        /*
        [Fact]
        public void TestingConection()
        { 
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            ClientMock.Setup(c => c.GetAsync<Response>(It.IsAny<string>())
            .Returns(Task.FromResult(httpResponseMessage)));

            _client = ClientMock.Object;
            var client = new HttpClientWrapper(_client);
        }
        */
        [Fact]
        public void Get_ReturnsTrue_IfTypeString()
        {
            ClientMock.Setup(r => r.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(() => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
            var service = new HttpClientWrapper();
            
            var result = service.Get(It.IsAny<string>());
            Assert.IsType<string>(result);
        }

        [Fact]
        public void GetBookByReference_ReturnsTrue_IfNotNull()
        {
            ClientMock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(new Book { Author = "tami" }));
            var operation = new GetBookByReferenceOperation(ClientMock.Object);
            var bookResponse = operation.GetBookByReference(It.IsAny<string>(), It.IsAny<string>());
            
            Assert.NotNull(bookResponse);
        }

        /*[Fact]
        public void Get_ReturnTrue_IfNull()
        {
            ClientMock.Setup(x => x.Get(It.IsNotNull<string>()))
                .Returns( new )
        }*/

        [Fact]
        public void Post_ReturnTrue_IfNotNull()
        {
            ClientMock.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(new HttpResponseMessage());

            var operation = new HttpClientWrapper();
            var result = operation.Post(It.IsAny<string>(), It.IsAny<HttpContent>());
            Assert.NotNull(result);
        }
    }
}
