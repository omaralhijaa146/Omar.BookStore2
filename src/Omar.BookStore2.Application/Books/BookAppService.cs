using Microsoft.AspNetCore.Authorization;
using Omar.BookStore2.Authors;
using Omar.BookStore2.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Omar.BookStore2.Books
{
    [Authorize(BookStore2Permissions.Books.Default)]
    public class BookAppService : ApplicationService,IBookAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookAppService(IBookRepository bookRepository,IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<BookDto> GetAsync(Guid id)
        {

            var result = await _bookRepository.GetAsync(id);

            if (result is null)
                throw new EntityNotFoundException(typeof(Book),id);

            var bookDto = ObjectMapper.Map<Book, BookDto>(result.Value.book);
            bookDto.AuthorName = result.Value.authorName;
            return bookDto;
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();
            return new ListResultDto<AuthorLookupDto>(ObjectMapper.Map<List<Author>,List<AuthorLookupDto>>(authors));
        }


        public async Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto bookListDto)
        {

            bookListDto.Sorting = NormalizeSorting(bookListDto.Sorting);

            var books = await _bookRepository.GetListAsync(bookListDto.SkipCount,bookListDto.MaxResultCount,bookListDto.Sorting,bookListDto.Filter,bookListDto.FilterByAuthor);

          
            if(!books.Any())
                return new PagedResultDto<BookDto>(0, []);

            var bookDtos = books.Select(x => {

                var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                bookDto.AuthorName = x.authorName;
                return bookDto;
            
            }).ToList();
            
            // since the books cannot exist without author we dont need to override the count method to join the table and then count
            var totalCount= await _bookRepository.GetCountAsync();

            return new PagedResultDto<BookDto>(totalCount,bookDtos);
        }

        [Authorize(BookStore2Permissions.Books.Create)]
        public async Task CreateAsync(CreateBookDto input)
        {
           if(input is null)
                throw new BusinessException("Invalid book. Book cannot be null");

            var book = ObjectMapper.Map<CreateBookDto, Book>(input);

            await _bookRepository.InsertAsync(book);
          
        }

        [Authorize(BookStore2Permissions.Books.Edit)]
        public async Task UpdateAsync(Guid id, UpdateBookDto input)
        {
           if(input is null) throw new BusinessException("Invalid book. Book cannot be null");

            if (input.Id != id)
                throw new BusinessException("Invalid book id").WithData("input", nameof(input));

            var oldBook= await _bookRepository.GetAsync(id);
            ObjectMapper.Map(input, oldBook.Value.book); 
            await _bookRepository.UpdateAsync(oldBook.Value.book);
        }
        
        [Authorize(BookStore2Permissions.Books.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var book= await _bookRepository.GetAsync(id);
            if (book is null)
                throw new BusinessException("Book does not exist");
            await _bookRepository.DeleteAsync(book.Value.book);
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrWhiteSpace()) return $"Book.{nameof(Book.Name)}";

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))

                return sorting.Replace("authorName", "AuthorName", StringComparison.OrdinalIgnoreCase);

            return $"Book.{sorting}";
        }
    }
}
