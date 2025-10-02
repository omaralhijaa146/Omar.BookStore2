using Microsoft.EntityFrameworkCore;
using Omar.BookStore2.Authors;
using Omar.BookStore2.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Omar.BookStore2.EntityFrameworkCore.Books
{
    public class EfCoreBookRepository : EfCoreRepository<BookStore2DbContext, Book, Guid>, IBookRepository
    {
        private readonly IAuthorRepository _authorRepository;

        public EfCoreBookRepository(IDbContextProvider<BookStore2DbContext> dbContextProvider,IAuthorRepository authorRepository) : base(dbContextProvider)
        {
            _authorRepository = authorRepository;
        }


        public async Task<(Book book,string authorName)?> GetAsync(Guid id)
        {

            var authorQueryable = await _authorRepository.GetQueryableAsync();
            var bookQueryable = await GetQueryableAsync();
            var query = from book in bookQueryable
                        join author in authorQueryable
                        on book.AuthorId equals author.Id
                        where book.Id == id
                        select new { Book = book, AuthorName = author.Name };

            var result= await query.FirstOrDefaultAsync();

            return result is not null?(result.Book,result.AuthorName):null;
        }

        public async Task<List<(Book book,string authorName)>?> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null,string filterByAuthor=null)
        {

            var bookQueryable = await GetQueryableAsync();
            var authorQueryable = await _authorRepository.GetQueryableAsync();

            var query = from book in bookQueryable
                        join author in authorQueryable
                        on book.AuthorId equals author.Id
                        select new { Book = book, AuthorName = author.Name };

            var pagedResult =await query
                .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                x=>x.Book.Name.Contains(filter))
                .WhereIf(
                !filterByAuthor.IsNullOrWhiteSpace(),
                x=>x.AuthorName.Contains(filterByAuthor))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount).ToListAsync();

            

            return pagedResult.Select(x=>(x.Book,x.AuthorName)).ToList();
        }
    }
}
