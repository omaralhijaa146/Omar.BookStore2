using AutoMapper;
using Omar.BookStore2.Authors;
using Omar.BookStore2.Books;
using Omar.BookStore2.Web.Pages.Books;

namespace Omar.BookStore2.Web;

public class BookStore2WebAutoMapperProfile : Profile
{
    public BookStore2WebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<BookDto,UpdateBookDto>();
        CreateMap<CreateAuthorViewModel,CreateAuthorDto>();
        CreateMap<AuthorDto, EditAuthorViewModel>();
        CreateMap<EditAuthorViewModel,UpdateAuthorDto>();
        CreateMap<BookDto, EditBookViewModel>();
        CreateMap<CreateBookViewModel, CreateBookDto>();
        CreateMap<EditBookViewModel, UpdateBookDto>();
        
    }
}
