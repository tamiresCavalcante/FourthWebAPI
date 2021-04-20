using FourthWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Operations.RemoveBook
{
    public class RemoveBookOperation
    {
        public RemoveBookOperation(IBookService service)
        {
            this.BookService = service;
        }

        private IBookService BookService;

        public void RemoveBooks(string id)
        {
            this.BookService.Remove(id);
        }
    }
}
