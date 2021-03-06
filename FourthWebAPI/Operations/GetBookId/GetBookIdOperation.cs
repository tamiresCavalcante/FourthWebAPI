using FourthWebAPI.Models;
using FourthWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Operations.GetBookId
{
    public class GetBookIdOperation
    {
        public GetBookIdOperation(IBookService service)
        {
            this.BookService = service;
        }

        private IBookService BookService;

        public Book GetBookId(string id)
        {
            return this.BookService.Get(id);
        }
    }
}
