using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;

namespace BookStore.UnitTests.Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper,_context);
            query.GenreId = 999;

            // act - assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldNotBeReturnError()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper, _context);
            query.GenreId = 1;

            //act
            FluentActions
                .Invoking(() => query.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == query.GenreId);
            genre.Should().NotBeNull();


        }
    }
}