using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Omar.BookStore2.Authors
{
    public class AuthorManager : DomainService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
    

    public async Task<Author> CreateAsync(string name, DateTime birthDate, string? shortBio)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name), AuthorConsts.MaxNameLength);
            var existingAuthor = await _authorRepository.FindByNameAsync(name);
            if (existingAuthor is not null)
                throw new AuthoeAlreadyExistsException(name);
            return new Author(GuidGenerator.Create(), name, birthDate, shortBio);
        }

        public async Task ChangeNameAsync(Author author,string newName) { 
        
            Check.NotNull(author, nameof(author));
            Check.NotNullOrWhiteSpace(newName,nameof(newName));

            var existingAuthor = await _authorRepository.FindByNameAsync(newName);
            if (existingAuthor is not null && existingAuthor.Id != author.Id)
                throw new AuthorAlreadyExistsException(newName);

            author.ChangeName(newName);
        }
    } 

}
