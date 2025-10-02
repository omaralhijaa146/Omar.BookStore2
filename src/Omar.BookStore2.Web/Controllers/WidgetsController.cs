using Microsoft.AspNetCore.Mvc;
using Omar.BookStore2.Books;
using Omar.BookStore2.Web.Components.BooksListWidgetServerRefreshViewComponent;
using Volo.Abp.AspNetCore.Mvc;

namespace Omar.BookStore2.Web.Controllers
{
    [Route("Widgets")]
    
    public class WidgetsController : AbpController
    {
        [Route("BooksServerSide")]
        [HttpGet]
        public IActionResult Index(string filter=null, string filterByAuthor=null, string sorting=null,int skipCount = 0, int maxResultCount = 10)
        {
            return ViewComponent(typeof(BookListWidgetServerRefreshViewComponent), new GetBookListDto { 
            
                Filter = filter,
                FilterByAuthor = filterByAuthor,
                MaxResultCount = maxResultCount,
                Sorting = sorting,
                SkipCount = skipCount
            });
        }
    }
}
