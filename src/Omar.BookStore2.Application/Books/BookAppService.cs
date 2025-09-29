using Microsoft.AspNetCore.Authorization;
using Omar.BookStore2.Authors;
using Omar.BookStore2.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Omar.BookStore2.Books
{
    [Authorize(BookStore2Permissions.Books.Default)]
    public class BookAppService : CrudAppService<Book,BookDto,Guid,PagedAndSortedResultRequestDto,CreateBookDto,UpdateBookDto>,IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;

        public BookAppService(IRepository<Book,Guid> repository,IAuthorRepository authorRepository):base(repository)
        {

            GetPolicyName = BookStore2Permissions.Books.Default;
            GetListPolicyName = BookStore2Permissions.Books.Default;
            CreatePolicyName = BookStore2Permissions.Books.Create;
            UpdatePolicyName = BookStore2Permissions.Books.Edit;
            DeletePolicyName = BookStore2Permissions.Books.Delete;
            _authorRepository = authorRepository;
        }

        public override async Task<BookDto> GetAsync(Guid id)
        {
            var bookQueryable = await Repository.GetQueryableAsync();
            var authorQueryable = await _authorRepository.GetQueryableAsync();

            var query = from book in bookQueryable
                        join author in authorQueryable
                        on book.AuthorId equals author.Id
                        where book.Id == id
                        select new { Book=book, AuthorName=author.Name };

            var result = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (result is null)
                throw new EntityNotFoundException(typeof(Book),id);

            var bookDto = ObjectMapper.Map<Book, BookDto>(result.Book);
            bookDto.AuthorName = result.AuthorName;
            return bookDto;
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();
            return new ListResultDto<AuthorLookupDto>(ObjectMapper.Map<List<Author>,List<AuthorLookupDto>>(authors));
        }


        public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var bookQueryable = await Repository.GetQueryableAsync();
            var authorQueryable = await _authorRepository.GetQueryableAsync();

            var query = from book in bookQueryable
                        join author in authorQueryable
                        on book.AuthorId equals author.Id
                        select new { Book = book, AuthorName = author.Name };

            query = query.OrderBy(NormalizeSorting(input.Sorting)).Skip(input.SkipCount).Take(input.MaxResultCount);

            var result= await AsyncExecuter.ToListAsync(query);

            var bookDtos = result.Select(x => {

                var bookDto = ObjectMapper.Map<Book, BookDto>(x.Book);
                bookDto.AuthorName = x.AuthorName;
                return bookDto;
            
            }).ToList();
            
            var totalCount= await Repository.GetCountAsync();

            return new PagedResultDto<BookDto>(totalCount,bookDtos);
        }
        private static string NormalizeSorting(string sorting)
        {
            if(sorting.IsNullOrWhiteSpace()) return $"book.{nameof(Book.Name)}";

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
            
                return sorting.Replace("authorName","author.Name",StringComparison.OrdinalIgnoreCase);
           
            return $"book.{sorting}" ;
        }

    }
}
