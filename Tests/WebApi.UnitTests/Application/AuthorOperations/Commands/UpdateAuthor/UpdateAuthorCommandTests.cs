using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.UpdateAuthor;
using WebApi.DBOperations;
using static WebApi.AuthorOperations.UpdateAuthor.UpdateAuthorCommand;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = 999;

            //act - assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Böyle bir yazar yok");
        }

        [Fact]
        public void WhenInvalidAuthorNameIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var author = new Author() {Name ="testupdateauthor",Surname = "test",Birthday = DateTime.Now.AddYears(-2)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorViewModel
            {
                Name = author.Name,
                Surname = author.Surname,
                Birthday = author.Birthday
            };

            //act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Aynı isimde yazar var");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdate()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            UpdateAuthorViewModel model = new UpdateAuthorViewModel{ Name = "test",Surname = "test2",Birthday = DateTime.Now.AddYears(-2)};

            command.Model = model;
            command.AuthorId = 1;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);
            author.Should().NotBeNull();
        }


    }
}