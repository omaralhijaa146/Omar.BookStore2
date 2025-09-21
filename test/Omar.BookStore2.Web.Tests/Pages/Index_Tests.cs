using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Omar.BookStore2.Pages;

[Collection(BookStore2TestConsts.CollectionDefinitionName)]
public class Index_Tests : BookStore2WebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
