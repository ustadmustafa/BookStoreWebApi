using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 099;

            //act - assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Bu id ile bir kategori yok");
        }
        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var genre = new Genre(){Name = "TestGenre"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel() {Name = genre.Name};

            //act - assert
                 FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Aynı isimde kategori var");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldNotBeReturnError()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel(){ Name = "TestUpdateGenre"};
            command.Model = model;
            command.GenreId = 1;

            //act 
                 FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}