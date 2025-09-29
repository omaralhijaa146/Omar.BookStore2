using Omar.BookStore2.Authors;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace Omar.BookStore2.Books
{
    public abstract class BookAppService_Tests<TStratupModule>:BookStore2ApplicationTestBase<TStratupModule>
        where TStratupModule : class, IAbpModule
    {
        private readonly IBookAppService _bookAppService;
        private readonly IAuthorAppService _authorAppService;

        private CreateBookDto CreateValidBookDto => new CreateBookDto
        {
            Name = "New test book 42",
            Price = 10,
            PublishDate = DateTime.Now,
            Type = BookType.ScienceFiction
        };

        private CreateBookDto CreateInValidBookDto => new CreateBookDto
        {
            Name = "New test book 42",
            Price = 10,
            PublishDate = DateTime.Now,
            Type = BookType.ScienceFiction
        };

        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
            _authorAppService = GetRequiredService<IAuthorAppService>();
        }

        [Theory]
        [InlineData("Name",10,0)]
        public async Task Should_Get_List_Of_books(string sorting,int maxResultCount,int skipCount) {

            var books = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto { 
            Sorting=sorting,
            MaxResultCount=maxResultCount,
            SkipCount= skipCount
            });
            books.TotalCount.ShouldBeGreaterThan(0);
            books.Items.ShouldContain(x=>x.Name=="1984"&&x.AuthorName=="George Orwell");
        }

        [Theory]
        [InlineData("New test book 42", 10, "2025-10-4", BookType.ScienceFiction)]

        [InlineData("", 10, "2025-10-4", BookType.ScienceFiction)]
        public async Task Should_Create_A_Valid_Book(string? name,float price,string publishDate,BookType type)
        {
            var authors = await _authorAppService.GetListAsync(new GetAuthorListDto());
            var firstAuthor= authors.Items.FirstOrDefault();
            var bookDto = new CreateBookDto
            {
                Name = name,
                Price = price,
                PublishDate = DateTime.Parse(publishDate),
                Type = type,
                AuthorId=firstAuthor.Id
            };

            if (string.IsNullOrEmpty(name))
            {
               var exception=await Assert.ThrowsAsync<AbpValidationException>(async () => { 
                await _bookAppService.CreateAsync(bookDto);
               });

                exception.ValidationErrors.ShouldContain(x=>x.MemberNames.Any(c=>c=="Name"));
            }
            else
            {
                var createdBook = await _bookAppService.CreateAsync(bookDto);
                createdBook.Id.ShouldNotBe(Guid.Empty);
                createdBook.Name.ShouldBe(name);
            }
          
        }




    }
}
