using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace BookStore.UnitTests.Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id)
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = id;

            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = 1;

            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}