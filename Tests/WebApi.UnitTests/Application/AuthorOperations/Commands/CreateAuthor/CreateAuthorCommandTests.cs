using Application.BookOperations.Commands.CreateBook;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TestSetup;
using WebApi.AuthorOperations.CreateAuthor;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var author = new Author() {Name = "Tyler Durden"};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorViewModel
            {
                Name = author.Name,
            };

            //act - assert
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Bu yazar zaten var");
        }

        [Fact]
        public void WhenValidInputIsGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorViewModel { Name = "Mustafa",Surname = "Albayrak",Birthday = DateTime.Now.AddYears(-2)};

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == command.Model.Name);
            author.Should().NotBeNull();
            author.Surname.Should().Be(command.Model.Surname);
            author.Birthday.Should().Be(command.Model.Birthday);
        }

    }
}