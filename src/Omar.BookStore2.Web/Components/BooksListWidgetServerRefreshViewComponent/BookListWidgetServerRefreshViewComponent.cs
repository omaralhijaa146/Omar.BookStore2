using Microsoft.AspNetCore.Mvc;
using Omar.BookStore2.Books;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Omar.BookStore2.Web.Components.BooksListWidgetServerRefreshViewComponent
{
    [Widget(RefreshUrl = "Widgets/BooksServerSide")]
    public class BookListWidgetServerRefreshViewComponent:AbpViewComponent
    {
        private readonly IBookAppService _bookAppService;

        public BookListWidgetServerRefreshViewComponent(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(GetBookListDto pagingDto) {
            
            var books = await _bookAppService.GetListAsync(pagingDto);
            return View("~/Components/BooksListWidgetServerRefreshViewComponent/Default.cshtml",books);
        }
    }
}
