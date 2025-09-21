using Omar.BookStore2.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Omar.BookStore2.Web.Pages;

public abstract class BookStore2PageModel : AbpPageModel
{
    protected BookStore2PageModel()
    {
        LocalizationResourceType = typeof(BookStore2Resource);
    }
}
