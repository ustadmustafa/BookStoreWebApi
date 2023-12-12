
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.BookOperations.DeleteBook;
using FluentAssertions;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.DeleteBooks
{
    public class DeleteBooksCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBooksCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
        {
            //arrange
            var book = new Book {Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};
            new Book {Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};
            new Book {Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};
            new Book {Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};

            _context.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            book = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            book.Should().BeNull();

        }
        
    }
}