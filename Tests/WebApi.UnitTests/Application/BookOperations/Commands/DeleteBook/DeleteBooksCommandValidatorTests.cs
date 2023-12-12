using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.DeleteBooks
{
    public class DeleteBooksCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(int id)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(2323)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}