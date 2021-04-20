using FourthWebAPI.Models;
using FourthWebAPI.Operations.GetBook;
using FourthWebAPI.Operations.GetBookId;
using FourthWebAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FourthWebApi.UnitTest
{
    public class BookServiceTest
    {
        private readonly Mock<IBookService> BookServiceMock = new Mock<IBookService>();
        [Fact]
        public void Get_ReturnsTrue_IftheCollectionIsNotEmpty()
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
        /*
        [Fact]
        public void GetId_ReturnsTrue_IfNotEmpty()
        {
            //Arrange
            var operation = new GetBookIdOperation(BookServiceMock.Object);
            Book book = new Book() { Id = "3" };
            BookServiceMock.Setup(x => x.Get()).Returns(new List<Book> { new Book { Id = It.IsAny<string>() } });
            //Act
            
            var result = operation.GetBookId("3");
        

            //Assert
            Assert.NotEmpty(result.Author);

        }*/

        [Fact]
        public void GetId_ReturnsTrue_IfNull()
        {
            var operation = new GetBookIdOperation(BookServiceMock.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                BookServiceMock.DefaultValueProvider = null;
            });
        }
    }
}
