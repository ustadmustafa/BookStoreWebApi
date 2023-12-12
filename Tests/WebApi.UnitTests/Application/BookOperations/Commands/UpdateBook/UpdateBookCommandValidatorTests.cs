using FluentAssertions;
using TestSetup;
using Webapi.BookOperations.UpdateBook;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.UpdateBooks
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("",0)]
        [InlineData("",1)]
        [InlineData("A",0)]
        [InlineData("A",1)]
        [InlineData("Aa",0)]
        [InlineData("AAA",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreid)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel{
                Title = title,
                GenreId = genreid
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel
            {
                Title = "AA",
                GenreId = 1
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}