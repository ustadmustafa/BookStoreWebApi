using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.CreateAuthor;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Mustafa","")]
        [InlineData("","Albayrak")]
        [InlineData("","")]
        [InlineData("Mustafa","Albayrak")]

        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnErrors(string name,string surname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorViewModel()
            {
                Name = name,
                Surname = surname
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateOfBirthTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorViewModel()
            {
                Name = "Mustafa",
                Surname = "Albayrak",
                Birthday = DateTime.Now.Date
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorViewModel()
            {
                Name = "Mustafa",
                Surname = "Albayrak",
                Birthday = DateTime.Now.Date.AddYears(-3)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}