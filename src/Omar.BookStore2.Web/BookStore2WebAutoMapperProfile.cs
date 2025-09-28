using AutoMapper;
using Omar.BookStore2.Authors;
using Omar.BookStore2.Books;

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
    }
}
