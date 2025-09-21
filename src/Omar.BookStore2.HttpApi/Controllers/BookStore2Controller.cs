using Omar.BookStore2.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Omar.BookStore2.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BookStore2Controller : AbpControllerBase
{
    protected BookStore2Controller()
    {
        LocalizationResource = typeof(BookStore2Resource);
    }
}
