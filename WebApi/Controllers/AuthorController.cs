using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentValidation.Results;
using FluentValidation;
using Webapi.BookOperations.UpdateBook;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.AuthorOperations.GetAuthors;
using WebApi.AuthorOperations.GetAuthorDetail;

using static WebApi.AuthorOperations.GetAuthorDetail.GetAuthorDetailQuery;
using WebApi.AuthorOperations.CreateAuthor;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;
using WebApi.AuthorOperations.UpdateAuthor;
using static WebApi.AuthorOperations.UpdateAuthor.UpdateAuthorCommand;
using WebApi.AuthorOperations.DeleteAuthor;
using WebApi.Application.AuthorOperations.DeleteAuthor;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAuthors(){
            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            AuthorDetailViewModel result;

                GetAuthorDetailQuery query = new GetAuthorDetailQuery(_mapper,_context);
                query.AuthorId = id;
                GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_mapper,_context);

            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete()]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

       
    }
}