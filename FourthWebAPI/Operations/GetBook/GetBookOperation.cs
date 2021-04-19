using FourthWebAPI.Models;
using FourthWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Operations.GetBook
{
    public class GetBookOperation
    {
        
        public GetBookOperation(IBookService service)
        {
            this.BookService = service;
        }

        private IBookService BookService;

        public List<Book> GetBooks()
        {
            return this.BookService.Get();
        }

    }
}
