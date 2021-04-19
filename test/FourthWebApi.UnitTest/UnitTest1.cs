using FourthWebAPI.Models;
using FourthWebAPI.Operations.GetBook;
using FourthWebAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FourthWebApi.UnitTest
{
    public class UnitTest1
    {
        private readonly Mock<IBookService> BookServiceMock = new Mock<IBookService>();
        [Fact]
        public void Test1()
        {
            //Arrange
            var operation = new GetBookOperation(BookServiceMock.Object);
            BookServiceMock.Setup(x => x.Get()).Returns(new List<Book> { 
            new Book { Author = It.IsAny<string>() } });

            //Act
            var result = operation.GetBooks();

            //Assert
            Assert.NotEmpty(result);
            BookServiceMock.Verify(x => x.Get(), Times.Once);
        }
    }
}
