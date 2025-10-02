using Microsoft.AspNetCore.Authorization;
using Omar.BookStore2.Books;
using Omar.BookStore2.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Omar.BookStore2.Authors
{
    [Authorize(BookStore2Permissions.Authors.Default)]
    public class AuthorAppService : BookStore2AppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorAppService(IAuthorRepository authorRepository,AuthorManager authorManager)
        {
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        [Authorize(BookStore2Permissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            var author = await _authorManager.CreateAsync(input.Name,input.BirthDate,input.ShortBio);
           
            await _authorRepository.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(BookStore2Permissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author = await _authorRepository.GetAsync(id);

            if(author.Id != id)
            {
                throw new Exception($"The author with id {id} is not found.");
            }

            if(author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }
            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;
            await _authorRepository.UpdateAsync(author);

        }

        [Authorize(BookStore2Permissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var userExists = await _authorRepository.FindAsync(id);
            if (userExists == null)
            {
                throw new Exception($"The author with id {id} is not found.");
            }
            await _authorRepository.DeleteAsync(id);
        }

        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author= await _authorRepository.GetAsync(id);
            var authorDto = ObjectMapper.Map<Author, AuthorDto>(author);
            return authorDto;
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if(input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authors= await _authorRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
                );
         

            var totalCount = input.Filter.IsNullOrWhiteSpace()?
                await _authorRepository.CountAsync():
                await _authorRepository.CountAsync(x=>x.Name.Contains(input.Filter));

            var pagedResultDto = new PagedResultDto<AuthorDto> {
            
                Items= ObjectMapper.Map<List<Author>,List<AuthorDto>>(authors),
                TotalCount=totalCount,
                
            };

            return pagedResultDto;
        }
    }
}
