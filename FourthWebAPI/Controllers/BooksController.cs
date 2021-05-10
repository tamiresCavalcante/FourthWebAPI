using FourthWebAPI.Models;
using FourthWebAPI.Operations.CreateBook;
using FourthWebAPI.Operations.CreateBookByClientId;
using FourthWebAPI.Operations.GetBook;
using FourthWebAPI.Operations.GetBookId;
using FourthWebAPI.Operations.RemoveBook;
using FourthWebAPI.Operations.UpdateBook;
using FourthWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FourthWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        //HttpClient httpClient;

        //public BooksController(HttpClient httpClient)
        //{
        //    this.httpClient = httpClient;
        //}

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            var operation = new GetBookOperation(_bookService);
            var result = operation.GetBooks();
            return result;
        }
            

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var operation = new GetBookIdOperation(_bookService);
            var result = operation.GetBookId(id);
  
            if(operation == null)
            {
                return NotFound();
            }
            
            return result;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            var operation = new CreateBookOperation(_bookService);
            var result = operation.CreateBooks(book);
            return result;
        }

        [HttpPut("update/{id}")]
        public ActionResult<string> Update([FromRoute]string id, [FromBody]Book bookIn)
        {
            var operation = new UpdateBookOperation(_bookService);
            
            if(operation == null)
            {
                return NotFound();
            }
            operation.UpdateBooks(id, bookIn);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<string> Delete([FromRoute] string id)
        {
            var operation = new RemoveBookOperation(_bookService);

            if(operation == null)
            {
                return NotFound();
            }
            operation.RemoveBooks(id);
        
            return id;
        }

        [HttpGet]
        public Book GetBookByReferenceController([FromRoute] string reference, string url)
        {
            GetBookByReferenceOperation operation = new GetBookByReferenceOperation(new HttpClientWrapper());
            Book result = operation.GetBookByReference(reference, url);
            return result;
        }
    }
}
