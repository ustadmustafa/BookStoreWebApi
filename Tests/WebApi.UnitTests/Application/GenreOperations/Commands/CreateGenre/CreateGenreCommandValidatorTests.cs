using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string genrename)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel
            {
                Name = genrename
            };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "TestGenreValidator"
            };

            command.Model = model;

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}