using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Quaries.GetBookDetails
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNonExistBookIdGiven_Get_ShouldBeReturnError()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 999;

            //act assert
            FluentActions   
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Bulunmadı");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Get_ShouldNotBeReturnError()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId=1;

            //act
            FluentActions
                .Invoking(() => query.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Id == query.BookId);
            book.Should().NotBeNull();
        }
    }
    
}