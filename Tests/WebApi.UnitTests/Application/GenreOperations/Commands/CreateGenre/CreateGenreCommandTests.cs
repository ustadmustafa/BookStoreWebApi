using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace BookStore.UnitTests.Application.GenreOperations.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleGiven_Genre_ShouldBeReturnError()
        {
            //arrange
            var genre = new Genre() {Name = "GenreTest"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){ Name = genre.Name};

            // act - assert
            FluentActions
                .Invoking(() =>command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kategori zaten mevcut");

        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated(){
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel(){Name = "GenreTest1"};

            command.Model = model;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }

}

