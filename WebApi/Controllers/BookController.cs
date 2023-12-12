using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentValidation.Results;
using FluentValidation;
using Webapi.BookOperations.UpdateBook;
using WebApi.Entities;



namespace WebApi.Controllers{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //private static List<Book> BookList = new List<Book>(){
            private readonly IBookStoreDbContext _context;
            private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //};

        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            BookDetailViewModel result;
            
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
           
            return Ok(result);
            


        }

        /*[HttpGet]
        public Book Get([FromQuery] string id){
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }*/

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                //FluentValidation.Results.ValidationResult result = validator.Validate(command);
                validator.ValidateAndThrow(command);
                command.Handle();

                // if(!result.IsValid)
                //     foreach (var item in result.Errors)
                //         Console.WriteLine("Özellik: " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
                // else
                //     command.Handle();

                
            
            
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
            
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            
            return Ok();
        }
    }

    /*public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; } 
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }*/

}