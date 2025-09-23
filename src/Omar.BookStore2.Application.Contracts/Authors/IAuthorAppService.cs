using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Omar.BookStore2.Authors
{
    public interface IAuthorAppService:IApplicationService
    {

        public Task<AuthorDto> GetAsync(Guid id);
        public Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);
        public Task<AuthorDto> CreateAsync (CreateAuthorDto input);
        public Task UpdateAsync (Guid id,UpdateAuthorDto input);

        public Task DeleteAsync(Guid id);

    }

}
