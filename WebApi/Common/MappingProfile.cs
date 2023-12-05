using AutoMapper;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;
using static WebApi.AuthorOperations.GetAuthorDetail.GetAuthorDetailQuery;
using static WebApi.AuthorOperations.GetAuthors.GetAuthorsQuery;
using static WebApi.AuthorOperations.UpdateAuthor.UpdateAuthorCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorViewModel, Author>();
            CreateMap<UpdateAuthorViewModel, Author>();

            
        }

    }
}