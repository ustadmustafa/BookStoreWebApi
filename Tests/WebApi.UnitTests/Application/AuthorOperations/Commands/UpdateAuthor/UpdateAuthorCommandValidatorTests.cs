using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.UpdateAuthor;
using static WebApi.AuthorOperations.UpdateAuthor.UpdateAuthorCommand;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("Mustafa","")]
        [InlineData("","Albayrak")]
        [InlineData("Mustafa","Albayrak")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null,null);
            UpdateAuthorViewModel model = new UpdateAuthorViewModel {Name = name,Surname = surname};

            command.AuthorId = 1;
            command.Model = model;

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }
    }
    
}