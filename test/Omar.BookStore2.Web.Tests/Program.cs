using Microsoft.AspNetCore.Builder;
using Omar.BookStore2;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Omar.BookStore2.Web.csproj"); 
await builder.RunAbpModuleAsync<BookStore2WebTestModule>(applicationName: "Omar.BookStore2.Web");

public partial class Program
{
}
