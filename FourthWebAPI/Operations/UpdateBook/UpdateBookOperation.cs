using FourthWebAPI.Models;
using FourthWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Operations.UpdateBook
{
    public class UpdateBookOperation
    {
        public UpdateBookOperation(IBookService service)
        {
            this.BookService = service;
        }
        private IBookService BookService;

        public void UpdateBooks(string id, Book bookIn)
        {
            this.BookService.Update(id, bookIn);
        }
    }
}
