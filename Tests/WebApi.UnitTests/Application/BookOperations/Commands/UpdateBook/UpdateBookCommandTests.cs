using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(BookStoreDbContext context)
        {
            _context = context;
        }

        [Fact]
        public void WhenNonExistBookIdGiven_Update_ShouldBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999;

            //act, assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("bu id ile bir kitap yok");
        }

        [Fact]
        public void WhenValidInputAreGiven_Update_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel {Title = "UpdateTest", GenreId = 1};

            command.Model = model;
            command.BookId = 1;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}