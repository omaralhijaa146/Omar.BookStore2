using AutoMapper;
using Omar.BookStore2.Authors;
using Omar.BookStore2.Books;

namespace Omar.BookStore2;

public class BookStore2ApplicationAutoMapperProfile : Profile
{
    public BookStore2ApplicationAutoMapperProfile()
    {

        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
