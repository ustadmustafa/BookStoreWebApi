using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.GetAuthorDetail;
using WebApi.DBOperations;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = 999;

            //act -assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Öyle Bir Yazar Yok!");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeGetDetails()
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = 1;

            //act
            FluentActions
                .Invoking(() => query.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault( x => x.Id == query.AuthorId);
            author.Should().NotBeNull();
        }
    }
}