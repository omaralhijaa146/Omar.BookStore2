using Omar.BookStore2.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Omar.BookStore2.Books
{
    public interface IBookRepository:IRepository<Book,Guid>
    {
        public Task<(Book book, string authorName)?> GetAsync(Guid id);
        public Task<List<(Book book,string authorName)>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null,string filterByAuthor=null);
    }
}
