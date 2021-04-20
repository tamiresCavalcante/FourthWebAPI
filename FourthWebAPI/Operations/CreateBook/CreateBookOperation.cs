using FourthWebAPI.Models;
using FourthWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Operations.CreateBook
{
    public class CreateBookOperation
    {
        public CreateBookOperation(IBookService service)
        {
            this.BookService = service;
        }

        private IBookService BookService;

        public Book CreateBooks(Book book)
        {
            return this.BookService.Create(book);
        }
    }
}
