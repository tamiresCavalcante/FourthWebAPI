using Autofac.Extras.Moq;
using FourthWebAPI.Models;
using FourthWebAPI.Operations.CreateBook;
using FourthWebAPI.Operations.GetBook;
using FourthWebAPI.Operations.GetBookId;
using FourthWebAPI.Operations.RemoveBook;
using FourthWebAPI.Operations.UpdateBook;
using FourthWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FourthWebApi.UnitTest
{
    public class BookServiceTest
    {
        private readonly Mock<IBookService> BookServiceMock = new Mock<IBookService>();
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

    }
}
