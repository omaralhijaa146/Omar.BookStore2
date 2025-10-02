using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Omar.BookStore2.Permissions.BookStore2Permissions;

namespace Omar.BookStore2.Books
{
    public interface IBookAppService:IApplicationService
    {

        public Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();

        public Task<BookDto> GetAsync(Guid id);

        public Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto bookListDto);
        public Task CreateAsync(CreateBookDto input);
        public Task UpdateAsync(Guid id, UpdateBookDto input);

        public Task DeleteAsync(Guid id);
    }
}
