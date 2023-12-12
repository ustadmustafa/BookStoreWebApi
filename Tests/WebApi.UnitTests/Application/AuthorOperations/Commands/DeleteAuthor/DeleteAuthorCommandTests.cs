using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 99;

            //act - assert
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("böyle yazar yok");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeDeleted(){
            //arrange
            var Author = new Author {Name = "Mustafa",Surname = "Albayrak"};
            _context.Authors.Add(Author);
            _context.SaveChanges();
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = Author.Id;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();       
                
            //assert
            Author = _context.Authors.SingleOrDefault(x  => x.Id ==command.AuthorId);
            Author.Should().BeNull();
    
         }
    }
}