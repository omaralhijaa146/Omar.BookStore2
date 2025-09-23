using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace Omar.BookStore2.Authors
{
    public abstract class AuthorAppService_Tests<TStartupModule> : BookStore2ApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IAuthorAppService _authorAppService;

        protected AuthorAppService_Tests()
        {
            _authorAppService = GetRequiredService<IAuthorAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Authors_Without_Any_Filter()
        {

            var result = await _authorAppService.GetListAsync(new GetAuthorListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(x => x.Name == "George Orwell");
            result.Items.ShouldContain(x => x.Name == "Douglas Adams");
            result.Items.ShouldContain(x => x.Name == "Omar Abu Al Hijaa");

        }

        [Fact]
        public async Task Should_Get_Filtered_Authors()
        {

            var requestl = new GetAuthorListDto() { Filter = "Omar" };
            var result = await _authorAppService.GetListAsync(requestl);
            result.TotalCount.ShouldBe(1);
            result.Items.ShouldContain(x => x.Name == "Omar Abu Al Hijaa");
            result.Items.ShouldNotContain(x => x.Name == "George Orwell");

        }

        [Fact]
        public async Task Should_Create_New_Author()
        {

            var request = new CreateAuthorDto
            {
                Name = "Omar Ibraheem",
                BirthDate = new DateTime(2000, 1, 1),
                ShortBio = "Short Bio"
            };

            var createdAuthor = await _authorAppService.CreateAsync(request);

            createdAuthor.Id.ShouldNotBe(Guid.Empty);
            createdAuthor.Name.ShouldBe(request.Name);

        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_Author()
        {

            var request = new CreateAuthorDto
            {
                Name = "Omar Abu Al Hijaa",
                BirthDate = new DateTime(2000, 1, 1)
            };

            await Assert.ThrowsAsync<AuthorAlreadyExistsException>(async () =>
            {
                await _authorAppService.CreateAsync(request);
            });

        }

        [Fact]
        public async Task Should_Update_Author()
        {

            var author = await _authorAppService.CreateAsync(
                new CreateAuthorDto
                {
                    Name = "Test Author1",
                    BirthDate = new DateTime(2000, 1, 1),
                    ShortBio = "Short Bio"
                });

            var updateRequest = new UpdateAuthorDto
            {
                Name = "Test Author Updated",
                BirthDate = new DateTime(1990, 1, 1),
                ShortBio = "Short Bio Updated"
            };

            await _authorAppService.UpdateAsync(author.Id, updateRequest);

            var updatedAuthor = await _authorAppService.GetAsync(author.Id);

            updatedAuthor.Name.ShouldBe(updateRequest.Name);
            updatedAuthor.BirthDate.ShouldBe(updateRequest.BirthDate);
            updatedAuthor.ShortBio.ShouldBe(updateRequest.ShortBio);

        }

        [Fact]
        public async Task Should_Delete_Author()
        {
            var author = await _authorAppService.CreateAsync(
                new CreateAuthorDto
                {
                    Name = "Test Author2",
                    BirthDate = new DateTime(2000, 1, 1),
                    ShortBio = "Short Bio"
                });
            await _authorAppService.DeleteAsync(author.Id);
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {

                await _authorAppService.GetAsync(author.Id);
            });


        }
    }
}
