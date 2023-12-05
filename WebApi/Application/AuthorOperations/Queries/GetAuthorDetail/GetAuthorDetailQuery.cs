
using System.Text.RegularExpressions;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.AuthorOperations.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IMapper mapper, BookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public AuthorDetailViewModel Handle(){
            var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);

            if(author is null)
                throw new InvalidOperationException("ID is not correct");

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

            return vm;
        }

        public class AuthorDetailViewModel{
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Birthday { get; set; }
        }
    }
}