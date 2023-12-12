using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 999;

            //act - assert
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("böyle bir kategori yok");

        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            //arrange
            var genre = new Genre {Name = "TEststsGenree"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            genre = _context.Genres.SingleOrDefault(x => x.Id == genre.Id);
            genre.Should().BeNull();
        }
    }
}